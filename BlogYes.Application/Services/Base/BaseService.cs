using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BlogYes.Application.Services.Base
{
    public class BaseService : IBaseService
    {
        public IMapper Mapper { get; init; } = null!;
        public ILogger<BaseService> Logger { get; set; } = null!;
    }
}
