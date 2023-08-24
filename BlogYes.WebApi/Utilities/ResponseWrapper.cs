using BlogYes.Application.Dtos;

namespace BlogYes.WebApi.Utilities
{
    public class ResponseWrapper<TRead>
    {
        public string? Info { get; set; }
        public TRead? Payload { get; set; }
        public int Status { get; set; } = StatusCodes.Status200OK; 
        public static ResponseWrapper<TRead> Create(TRead read, string? info = null, int status = StatusCodes.Status200OK)
        {
            return new ResponseWrapper<TRead>()
            {
                Info = info,
                Payload = read,
                Status = status
            };
        }
    }
    public static class ReadDtoWrapperExtension
    {
        public static ResponseWrapper<UserReadDto?> Wrap(this UserReadDto? read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<UserReadDto?>()
            {
                Info = info,
                Payload = read,
                Status = status
            };

        public static ResponseWrapper<IEnumerable<UserReadDto>> Wrap(this IEnumerable<UserReadDto> read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<IEnumerable<UserReadDto>>()
            {
                Info = info,
                Payload = read,
                Status = status
            };

        public static ResponseWrapper<BlogReadDto?> Wrap(this BlogReadDto? read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<BlogReadDto?>()
            {
                Info = info,
                Payload = read,
                Status = status
            };

        public static ResponseWrapper<IEnumerable<BlogReadDto>> Wrap(this IEnumerable<BlogReadDto> read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<IEnumerable<BlogReadDto>>()
            {
                Info = info,
                Payload = read,
                Status = status
            };

        public static ResponseWrapper<CaptchaReadDto?> Wrap(this CaptchaReadDto? read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<CaptchaReadDto?>()
            {
                Info = info,
                Payload = read,
                Status = status
            };

        public static ResponseWrapper<RoleReadDto?> Wrap(this RoleReadDto? read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<RoleReadDto?>()
            {
                Info = info,
                Payload = read,
                Status = status
            };

        public static ResponseWrapper<IEnumerable<RoleReadDto>> Wrap(this IEnumerable<RoleReadDto> read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<IEnumerable<RoleReadDto>>()
            {
                Info = info,
                Payload = read,
                Status = status
            };
        public static ResponseWrapper<IEnumerable<RoleScopeReadDto>> Wrap(this IEnumerable<RoleScopeReadDto> read,
            string? info = null, int status = StatusCodes.Status200OK) =>
            new ResponseWrapper<IEnumerable<RoleScopeReadDto>>()
            {
                Info = info,
                Payload = read,
                Status = status
            };
    }
}
