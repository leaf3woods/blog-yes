using BlogYes.Application.Dtos;
using BlogYes.Core.Exceptions;
using BlogYes.Core.Utilities;

namespace BlogYes.WebApi.Exceptions
{
    public static class ExceptionExtension
    {
        public static ExceptionReadDto Localize(this Exception exception)
        {
            var result = exception switch
            {
                NotFoundException or NotAcceptableException or ForbiddenException => new ExceptionReadDto() { Message = string.Format(exception.Message) },
                _ => new ExceptionReadDto
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    Inner = exception.InnerException?.Message
                }
            };
            if (!SettingUtil.IsDevelopment)
            {
                var index = result.Message.IndexOf('\r');
                result.Message = result.Message[..index];
                result.StackTrace = null;
                result.Inner = null;
            }
            return result;
        }
    }
}
