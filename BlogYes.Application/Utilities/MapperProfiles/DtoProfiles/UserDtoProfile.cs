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
            CreateMap<UserRegisterDto, User>()
                .AfterMap((src, dest) =>
                {
                    var bytes = Convert.FromBase64String(src.Password);
                    dest.Passphrase = Convert.ToBase64String(CryptoUtil.Salt(bytes, out var salt));
                    dest.Salt = Convert.ToBase64String(salt);
                    dest.RoleId = Role.MemberRole.Id;
                });
            CreateMap<Setting, UserSettingReadDto>();
            CreateMap<Setting, UserDetailReadDto>();
        }
    }
}