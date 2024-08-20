using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions in the application.
    /// Primary Constructor initializes the ExceptionMiddleware.
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger to log exceptions.</param>
    /// <param name="env">The hosting environment.</param>
    /// </summary>
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        /// <summary>
        /// Invokes the middleware to handle exceptions.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the completion of request processing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal server error");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}