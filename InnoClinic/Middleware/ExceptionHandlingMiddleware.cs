using System.Text.Json;
using InnoClinic.Domain.Exceptions;

namespace InnoClinic.Middleware
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case ClinicException clinicException:
                        await HandleExceptionAsync(context, clinicException);
                        break;
                    default:
                        await HandleExceptionAsync(context, e);
                        break;
                }
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, ClinicException exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                errorCode = exception.ErrorCode.ToString(),
                errorMessage = exception.Message
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                errorCode = httpContext.Response.StatusCode.ToString(),
                errorMessage = exception.Message
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
