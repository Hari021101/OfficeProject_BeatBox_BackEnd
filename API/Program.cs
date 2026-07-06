using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog Logging
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=BeatBoxDb;Trusted_Connection=True;";

var sinkOptions = new MSSqlServerSinkOptions
{
    TableName = "SerilogLogs",
    AutoCreateSqlTable = true
};

var loggerConfig = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/beatbox-log-.txt", rollingInterval: RollingInterval.Day);

// Only log to SQL Server in production to avoid crashing if the local database isn't created yet
if (!builder.Environment.IsDevelopment())
{
    loggerConfig.WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: sinkOptions);
}

Log.Logger = loggerConfig.CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<API.Middleware.InputSanitizationFilter>();
});

builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();

// Response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProvider>();
    options.Providers.Add<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProvider>();
});

builder.Services.Configure<Microsoft.AspNetCore.ResponseCompression.BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<Microsoft.AspNetCore.ResponseCompression.GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

QuestPDF.Settings.License = LicenseType.Community;
// Register Clean Architecture Infrastructure Services (DbContext, Identity, JWT, etc.)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add CORS Policy for Vite React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin(); // Allow all origins for the live API
    });
});


// Configure Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token like: Bearer {your token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

// Configure forwarded headers for MonsterASP reverse proxy SSL
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

app.UseResponseCompression();

app.UseStaticFiles();

// Automatically apply pending EF migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogWarning(ex, "An warning or error occurred while applying database migrations. Proceeding to seeding.");
        }
        
        var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Domain.Entities.AppUser>>();
        var roleManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
        // Initialize content root path for local image seeding and validation
        DbSeeder.SetContentRootPath(app.Environment.ContentRootPath);
        DbSeeder.InitializeImagePools();

        // Seed high-fidelity e-commerce catalog data if empty
        await DbSeeder.SeedAsync(context, userManager, roleManager);

    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(); // Maps Swagger UI playground at /swagger/index.html

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseResponseCaching();

// Enable serving static files from wwwroot
app.UseStaticFiles();

// Apply CORS Policy
app.UseCors("CorsPolicy");

// Enable Authentication and Authorization middlewares
app.UseAuthentication();
app.UseAuthorization();

// Map SignalR hubs
app.MapHub<Infrastructure.SignalR.NotificationHub>("/hubs/notifications");
app.MapHub<Infrastructure.SignalR.OrderTrackingHub>("/hubs/orders");

// Map controllers (e.g. AccountController)
app.MapControllers();

// Map fallback to serve the React SPA
app.MapFallbackToFile("index.html");

app.Run();
