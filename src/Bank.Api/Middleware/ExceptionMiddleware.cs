using Bank.Application.Exceptions;
using Bank.Application.Models;
using System.Net;
using System.Text.Json;

namespace Bank.Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
    private readonly IWebHostEnvironment _env = env;


    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomerActionInputException iex)
        {
            if (iex.InputError is { })
            {
                _logger.LogError($"{iex.Message} {iex.InputError.ToString()}");
            }
            await HandleInputExceptionAsync(httpContext, iex, iex.InputError);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an error: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleInputExceptionAsync(HttpContext context, Exception exception, InputError? inputError)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var response = new { message = exception.Message, inputError = _env.IsDevelopment() ? inputError : null };

        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new { message = exception.Message, innerMessage = exception.InnerException?.Message, stackTrace = _env.IsDevelopment() ? exception.StackTrace : null };

        var result = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(result);
    }


}
