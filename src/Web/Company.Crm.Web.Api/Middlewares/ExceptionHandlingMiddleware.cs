using Company.Framework.Dtos;
using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Company.Crm.Web.Api.Middlewares;

public static class GlobalExceptionHandlingMiddlewareExtensions
{
    public static void UseGlobalExceptionHandling(this IApplicationBuilder app, bool isProduction)
    {
        app.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorMessage = "Error!";

            var error = context.Features.Get<IExceptionHandlerPathFeature>();

            var exception = error?.Error;

            if (exception is FileNotFoundException)
            {
                errorMessage = "File Not Found!";
            }
            else if (exception is UnauthorizedAccessException)
            {
                //context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                errorMessage = "Unauthorized Access to resource!";
            }
            else if (exception is ValidationException)
            {
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                errorMessage = "Validation error!";
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                errorMessage = "Internal error!";
            }

            if (!isProduction) errorMessage = exception.Message;

            var errorResponse = new ServiceResponse<string>(errorMessage);
            var errorBody = JsonSerializer.Serialize(errorResponse);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorBody);
        });
    }
}
