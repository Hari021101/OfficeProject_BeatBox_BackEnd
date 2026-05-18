using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Clean Architecture Infrastructure Services (DbContext, Identity, JWT, etc.)
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add CORS Policy for Vite React Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:5173");
    });
});

// Configure Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

// Redirect root to Swagger UI so user never sees a 404
app.MapGet("/", async context =>
{
    context.Response.Redirect("/swagger");
    await Task.CompletedTask;
});

// Map controllers (e.g. AccountController)
app.MapControllers();

app.Run();
