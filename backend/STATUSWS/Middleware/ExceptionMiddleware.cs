using StatusWS.Errors;
using System.Net;
using System.Text.Json;

namespace StatusWS.Middleware
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            //(404)
            catch (NotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, notFoundEx.Message);
                await HandleExceptionAsync(context, notFoundEx, HttpStatusCode.NotFound);
            }
            //(400)
            catch (BadRequestException badRequestEx)
            {
                _logger.LogWarning(badRequestEx, badRequestEx.Message);
                await HandleExceptionAsync(context, badRequestEx, HttpStatusCode.BadRequest);
            }
            //(401)
            catch (UnauthorizedException unauthorizedEx)
            {
                _logger.LogWarning(unauthorizedEx, unauthorizedEx.Message);
                await HandleExceptionAsync(context, unauthorizedEx, HttpStatusCode.Unauthorized);
            }
            // GENÉRICA (500)
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var message = exception.Message;
            var details = _env.IsDevelopment() ? exception.StackTrace?.ToString() : "Internal server error";

            if (statusCode != HttpStatusCode.InternalServerError && !_env.IsDevelopment())
            {
                details = message;
            }

            var response = new ApiException(context.Response.StatusCode.ToString(), message, details);
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }

}

