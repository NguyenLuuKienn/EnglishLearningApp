using System.Net;
using System.Text.Json;
using EnglishLearning.WebAPI.Models.Common;

namespace EnglishLearning.WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

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
