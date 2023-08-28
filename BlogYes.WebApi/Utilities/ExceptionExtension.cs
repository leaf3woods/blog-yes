using BlogYes.Application.Dtos;
using BlogYes.Core.Exceptions;
using BlogYes.Core.Utilities;
using Microsoft.Extensions.Localization;

namespace BlogYes.WebApi.Exceptions
{
    public static class ExceptionExtension
    {
        public static IStringLocalizer Localizer = null!;
        public static ExceptionReadDto Localize(this Exception exception)
        {
            var index = exception.Message.IndexOf('\r');
            var result = exception switch
            {
                NotFoundException or NotAcceptableException or ForbiddenException => new ExceptionReadDto() { Info = Localizer[(exception as CustomException)!.ExceptionCode]},
                _ => SettingUtil.IsDevelopment ? new ExceptionReadDto
                {
                    Info = exception.Message,
                    StackTrace = exception.StackTrace,
                    Inner = exception.InnerException?.Message
                } : new ExceptionReadDto
                {
                    Info = index == -1 ? exception.Message : exception.Message[..index],
                    StackTrace = null,
                    Inner = null
                }
            };
            return result;
        }
    }
}
