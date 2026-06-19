using System.Text;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Repositories;
using Application.Common.Mappings;
using Infrastructure.SignalR;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                          .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            // Register ASP.NET Core Identity
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = false;
            })
           .AddEntityFrameworkStores<AppDbContext>()
           .AddDefaultTokenProviders();

            // Register Token Service
            services.AddScoped<ITokenService, TokenService>();

            // Register JWT Authentication
            var jwtKey = configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT Key is missing from configuration.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"].FirstOrDefault();
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs/notifications") ||
                             path.StartsWithSegments("/hubs/orders")))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();
            services.AddScoped<IAdminDashboardService, AdminDashboardService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            
            // Phase 7
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<ICouponRepository, CouponRepository>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IRazorpayService, RazorpayService>();

            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IReturnService, ReturnService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            services.AddScoped<
                INotificationManagerService,
                NotificationManagerService>();

            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ProductProfile).Assembly, typeof(InventoryProfile).Assembly));

            // Register SignalR hubs
            services.AddSignalR();

            // OTP Service (Email via MailKit, Phone via console)
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
