using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.UserDto;

namespace Saas.Mappings
{
    public class UserMap : Profile{
        public UserMap(){

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail));

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.UserEmail))
                .ForMember(dest => dest.UserPhone, opt => opt.MapFrom(src => src.UserPhone))
                .ForMember(dest => dest.UserCpf, opt => opt.MapFrom(src => src.UserCpf));
        }
    }
}
