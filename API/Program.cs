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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/beatbox-log-.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: sinkOptions)
    .CreateLogger();

builder.Host.UseSerilog();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Maps Swagger UI playground at /swagger/index.html
}

app.UseHttpsRedirection();

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

// Redirect root to Swagger UI so user never sees a 404
app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

// Map controllers (e.g. AccountController)
app.MapControllers();

app.Run();
