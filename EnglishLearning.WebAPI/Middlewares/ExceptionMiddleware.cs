using System.Net;
using System.Text.Json;
using EnglishLearning.WebAPI.Models.Common;

namespace EnglishLearning.WebAPI.Middlewares;

public class ExceptionMiddleware(
    RequestDelegate _next,
    ILogger<ExceptionMiddleware> _logger,
    IHostEnvironment _environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            (HttpStatusCode code, string message) = ex switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, ex.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
                InvalidOperationException when ex.Message.Contains("already exists", StringComparison.OrdinalIgnoreCase)
                    => (HttpStatusCode.Conflict, ex.Message),
                _ => (HttpStatusCode.InternalServerError, "An internal server error occurred")
            };

            response.StatusCode = (int)code;

            var result = ApiResponse<object>.BadRequest(
                new List<string> { _environment.IsDevelopment() ? ex.ToString() : message },
                message);

            var json = JsonSerializer.Serialize(result);
            await response.WriteAsync(json);
        }
    }
}

// Extension method
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}
