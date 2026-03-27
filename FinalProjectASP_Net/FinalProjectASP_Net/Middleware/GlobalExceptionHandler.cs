using FinalProjectASP_Net.Core.Exceptions;
using FinalProjectASP_Net.Core.Models.ResponseModels;
using System.Text.Json;

namespace FinalProjectASP_Net.Middleware
{
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occured");

            var statusCode = ex switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,

                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                UnauthorizedException => StatusCodes.Status401Unauthorized,

                ForbiddenException => StatusCodes.Status403Forbidden,

                UserNotFoundException => StatusCodes.Status404NotFound,
                VacancyNotFoundException => StatusCodes.Status404NotFound,
                ResourceNotFoundException => StatusCodes.Status404NotFound,

                EmailAlreadyTakenException => StatusCodes.Status409Conflict,
                EntityAlreadyExistsException => StatusCodes.Status409Conflict,

                OperationFailedException => StatusCodes.Status400BadRequest,

                _ => StatusCodes.Status500InternalServerError
            };

            var error = CreateError(context, ex, statusCode);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(error);

            await context.Response.WriteAsync(json);
        }

        private ErrorResponse CreateError(HttpContext context, Exception ex, int statusCode)
        {
            return new ErrorResponse
            {
                StatusCode = statusCode,
                Message = ex.Message,
                TraceId = context.TraceIdentifier,
                Detail = _environment.IsDevelopment() ? ex.StackTrace : null
            };
        }
    }

   
}
