using System.Net;
using System.Text.Json;
using EnglishLearning.WebAPI.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EnglishLearnningApp.UnitTest.WebAPI.Middleware;

public class ExceptionMiddlewareTests
{
    private HttpContext CreateHttpContext()
    {
        var context = new DefaultHttpContext();
        var response = new System.IO.MemoryStream();
        context.Response.Body = response;
        return context;
    }

    private IHostEnvironment CreateEnvironment(bool isDevelopment)
    {
        var env = new Mock<IHostEnvironment>();
        // IsDevelopment() is an extension method that checks EnvironmentName == "Development"
        env.Setup(e => e.EnvironmentName).Returns(isDevelopment ? "Development" : "Production");
        return env.Object;
    }

    [Fact]
    public async Task InvokeAsync_ArgumentException_ShouldReturnBadRequest()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new ArgumentException("Invalid argument");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task InvokeAsync_KeyNotFoundException_ShouldReturnNotFound()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new KeyNotFoundException("Resource not found");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task InvokeAsync_UnauthorizedAccessException_ShouldReturnUnauthorized()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new UnauthorizedAccessException("Unauthorized");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task InvokeAsync_InvalidOperationExceptionWithAlreadyExists_ShouldReturnConflict()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new InvalidOperationException("Username already exists");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task InvokeAsync_InvalidOperationExceptionWithoutAlreadyExists_ShouldReturnInternalServerError()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new InvalidOperationException("Something went wrong");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task InvokeAsync_GenericException_ShouldReturnInternalServerError()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new Exception("Unexpected error");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task InvokeAsync_NoException_ShouldCallNextDelegate()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        bool nextCalled = false;
        RequestDelegate next = (ctx) =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        };

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        nextCalled.Should().BeTrue();
    }

    [Fact]
    public async Task InvokeAsync_InDevelopmentMode_ShouldIncludeStackTrace()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(true);

        RequestDelegate next = (ctx) => throw new KeyNotFoundException("Not found");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
        using var reader = new System.IO.StreamReader(context.Response.Body);
        var responseText = await reader.ReadToEndAsync();

        // ex.ToString() includes the full type name "System.Collections.Generic.KeyNotFoundException"
        responseText.Should().Contain("KeyNotFoundException");
    }

    [Fact]
    public async Task InvokeAsync_ShouldSetContentTypeToJson()
    {
        var context = CreateHttpContext();
        var logger = new Mock<ILogger<ExceptionMiddleware>>();
        var environment = CreateEnvironment(false);

        RequestDelegate next = (ctx) => throw new ArgumentException("Error");

        var middleware = new ExceptionMiddleware(next, logger.Object, environment);
        await middleware.InvokeAsync(context);

        context.Response.ContentType.Should().Be("application/json");
    }
}
