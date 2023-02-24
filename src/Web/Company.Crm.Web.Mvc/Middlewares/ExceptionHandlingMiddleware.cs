using Company.Framework.Dtos;
using System.Net;

namespace Company.Crm.Web.Mvc.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            catch (Exception exception)
            {
                HandleException(context, exception);
            }
        }

        private void HandleException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "text/html";

            var errorResponse = new ServiceResponse<string>(exception.Message);

            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    }
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException ex:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal Server errors. Check Logs!";
                    break;
            }

            _logger.LogError(exception.Message, exception);

            //await context.Response.WriteAsync(errorResponse.Message);
            context.Response.Redirect("/Home/Error");
        }
    }

    public static class GlobalExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
