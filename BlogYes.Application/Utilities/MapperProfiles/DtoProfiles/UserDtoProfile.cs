using AutoMapper;
using BlogYes.Application.Dtos;
using BlogYes.Domain.Entities;
using BlogYes.Domain.ValueObjects.UserValue;

namespace BlogYes.Application.Utilities.MapperProfiles.DtoProfiles
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<Setting, UserSettingReadDto>();
            CreateMap<Setting, UserDetailReadDto>();
        }
    }
}
