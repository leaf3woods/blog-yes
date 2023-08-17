using AutoMapper;
using BlogYes.Application.Dtos;
using BlogYes.Domain.Entities;

namespace BlogYes.Application.Utilities.MapperProfiles.DtoProfiles
{
    public class BlogDtoProfile : Profile
    {
        public BlogDtoProfile()
        {
            CreateMap<Blog, UserReadDto>();
            CreateMap<BlogUpdateDto, User>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}