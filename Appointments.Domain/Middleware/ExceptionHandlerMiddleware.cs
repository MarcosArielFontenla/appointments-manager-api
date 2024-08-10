using Appointments.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace Appointments.Domain.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
                _logger.LogError($"TraceId: {context.TraceIdentifier}, Error: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            response.StatusCode = exception switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status400BadRequest,
                ApplicationException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                NotImplementedException => StatusCodes.Status501NotImplemented,
                TimeoutException => StatusCodes.Status408RequestTimeout,
                InvalidOperationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var exceptionResponse = new ExceptionResponse
            {
                StatusCode = response.StatusCode,
                Message = exception.Message,
                Detail = $"Error occurred during {context.Request.Method} on {context.Request.Path}. TraceId: {context.TraceIdentifier}",
                Instance = context.Request.Path,
                Type = exception.GetType().Name,
                StackTrace = exception.StackTrace ?? string.Empty
            };

            var result = JsonSerializer.Serialize(exceptionResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
