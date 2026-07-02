using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace API.Middleware;

public static class HtmlSanitizer
{
    private static readonly Regex HtmlRegex = new("<.*?>", RegexOptions.Compiled);

    public static string Sanitize(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        // Decode HTML entities first to prevent obfuscated payloads (e.g., &lt;script&gt;)
        var decoded = HttpUtility.HtmlDecode(input);

        // Strip HTML/Script tags
        var sanitized = HtmlRegex.Replace(decoded, string.Empty);

        return sanitized.Trim();
    }
}

public class InputSanitizationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var key in context.ActionArguments.Keys.ToList())
        {
            var argument = context.ActionArguments[key];
            if (argument == null) continue;

            if (argument is string strValue)
            {
                context.ActionArguments[key] = HtmlSanitizer.Sanitize(strValue);
            }
            else if (argument.GetType().IsClass && argument.GetType() != typeof(string))
            {
                SanitizeObject(argument);
            }
        }

        await next();
    }

    private void SanitizeObject(object obj)
    {
        if (obj == null) return;

        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in properties)
        {
            if (prop.PropertyType == typeof(string) && prop.CanWrite && prop.CanRead)
            {
                var value = (string)prop.GetValue(obj);
                if (value != null)
                {
                    prop.SetValue(obj, HtmlSanitizer.Sanitize(value));
                }
            }
            else if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string) && prop.CanRead)
            {
                // Prevent infinite recursion for circular dependencies (if any)
                if (prop.PropertyType == obj.GetType()) continue;
                
                var value = prop.GetValue(obj);
                if (value != null)
                {
                    SanitizeObject(value);
                }
            }
        }
    }
}
