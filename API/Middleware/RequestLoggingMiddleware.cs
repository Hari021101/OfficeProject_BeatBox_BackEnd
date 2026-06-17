using System.Diagnostics;
using System.Security.Claims;

namespace API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var request = context.Request;
        var method = request.Method;
        var path = request.Path;
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        await _next(context);

        stopwatch.Stop();
        var statusCode = context.Response.StatusCode;
        var elapsedMs = stopwatch.ElapsedMilliseconds;

        var user = context.User;
        var userId = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
        var userEmail = user?.FindFirst(ClaimTypes.Email)?.Value ?? "Anonymous";

        // Write structured logs for API & User Activity tracking
        if (userId != "Anonymous")
        {
            _logger.LogInformation(
                "USER ACTIVITY: User {UserId} ({UserEmail}) performed {Method} {Path} from IP {IpAddress}. Response: {StatusCode} in {ElapsedMs}ms",
                userId, userEmail, method, path, ipAddress, statusCode, elapsedMs);
        }
        else
        {
            _logger.LogInformation(
                "API REQUEST: {Method} {Path} from IP {IpAddress} responded {StatusCode} in {ElapsedMs}ms",
                method, path, ipAddress, statusCode, elapsedMs);
        }
    }
}
