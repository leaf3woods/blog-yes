using BlogYes.Core.Exceptions;
using BlogYes.Core;
using BlogYes.WebApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BlogYes.WebApi.Utilities
{
    public static class ExceptionLocalizerExtension
    {
        public static async Task LocalizeException(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var exception = context.Features.Get<IExceptionHandlerFeature>();            
            if (exception != null)
            {                
                context.Response.StatusCode = exception.Error switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    NotAcceptableException => StatusCodes.Status406NotAcceptable,
                    _ => StatusCodes.Status500InternalServerError
                };
                
                var errDto = exception.Error.Localize();
                await context.Response.WriteAsJsonAsync(errDto, Options.CustomJsonSerializerOptions);
            }
        }
    }
}
