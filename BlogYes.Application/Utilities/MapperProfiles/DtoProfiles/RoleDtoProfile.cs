using AutoMapper;
using BlogYes.Application.Dtos;
using BlogYes.Domain.Entities;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Application.Utilities.MapperProfiles.DtoProfiles
{
    public class RoleDtoProfile : Profile
    {
        public RoleDtoProfile()
        {
            CreateMap<RoleCreateDto, Role>();
            CreateMap<Role, RoleReadDto>();
            CreateMap<Scope, RoleScopeReadDto>();
            CreateMap<RoleScopeModifyDto, Scope>();
        }
    }
}
