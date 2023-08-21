using AutoMapper;
using BlogYes.Application.Dtos;
using BlogYes.Domain.Entities;

namespace BlogYes.Application.Utilities.MapperProfiles.DtoProfiles
{
    public class RoleDtoProfile : Profile
    {
        public RoleDtoProfile()
        {
            CreateMap<RoleCreateDto, Role>()
                .ForMember(dest => dest.Scopes, opt => opt.Ignore());
            CreateMap<Role, RoleReadDto>();
            CreateMap<Scope, RoleScopeReadDto>();
        }
    }
}