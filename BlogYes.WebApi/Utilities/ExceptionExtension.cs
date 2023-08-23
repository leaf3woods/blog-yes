using BlogYes.Application.Dtos;
using BlogYes.Core.Exceptions;

namespace BlogYes.WebApi.Exceptions
{
    public static class ExceptionExtension
    {
        public static ExceptionReadDto Localize(this Exception exception)
        {
            return exception switch
            {
                NotFoundException or NotAcceptableException =>  new ExceptionReadDto() { Message = string.Format(exception.Message) } ,
                _ => new ExceptionReadDto
                { 
                    Message = exception.Message,
                    StackTrace = exception.StackTrace ?? string.Empty,
                    Inner = exception.InnerException?.Message ?? string.Empty
                }
            };
        }
    }
}
