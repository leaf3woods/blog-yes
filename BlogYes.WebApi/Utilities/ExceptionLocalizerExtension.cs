using BlogYes.Core;
using BlogYes.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace BlogYes.WebApi.Utilities
{
    public static class ExceptionLocalizerExtension
    {
        public static async Task LocalizeException(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var exception = context.Features.Get<IExceptionHandlerFeature>();
            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                NotAcceptableException => StatusCodes.Status406NotAcceptable,
                _ => StatusCodes.Status500InternalServerError
            };
            exception
            if (exception != null)
            {
                var error = new ErrorMessage()
                {
                    Stacktrace = exception.Error.StackTrace,
                    Message = exception.Error.Message
                };
                var errObj = JsonSerializer.Serialize(error, Options.JsonSerializerOptions);

                await context.Response.WriteAsync(errObj).ConfigureAwait(false);

            }
        }
    }
}
