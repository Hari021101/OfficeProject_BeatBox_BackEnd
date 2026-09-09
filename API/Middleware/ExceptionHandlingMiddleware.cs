using System.Net;
using System.Text.Json;
using Application.DTOs;

namespace API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred during request {Method} {Path}: {Message}", 
                context.Request.Method, context.Request.Path, ex.Message);
                
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var isStockConflict = exception is InvalidOperationException && 
                              (exception.Message.Contains("available", StringComparison.OrdinalIgnoreCase) || 
                               exception.Message.Contains("stock", StringComparison.OrdinalIgnoreCase));

        var statusCode = isStockConflict ? HttpStatusCode.Conflict : exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new ApiResponse<object>
        {
            Success = false,
            Message = exception.Message,
            Data = null
        };

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var result = JsonSerializer.Serialize(response, jsonOptions);
        return context.Response.WriteAsync(result);
    }
}
