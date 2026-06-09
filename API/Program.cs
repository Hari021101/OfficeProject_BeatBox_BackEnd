using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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
              .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
              .AllowCredentials();
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

// Automatically apply pending EF migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        
        var userManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Domain.Entities.AppUser>>();
        var roleManager = services.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole>>();
        
        // Seed high-fidelity e-commerce catalog data if empty
        await DbSeeder.SeedAsync(context, userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating and seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Maps Swagger UI playground at /swagger/index.html
}

app.UseHttpsRedirection();

// Apply CORS Policy
app.UseCors("CorsPolicy");

// Enable Authentication and Authorization middlewares
app.UseAuthentication();
app.UseAuthorization();

// Map SignalR hubs
app.MapHub<Infrastructure.SignalR.NotificationHub>("/hubs/notifications");
app.MapHub<Infrastructure.SignalR.OrderTrackingHub>("/hubs/orders");

// Redirect root to Swagger UI so user never sees a 404
app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

// Map controllers (e.g. AccountController)
app.MapControllers();

app.Run();
