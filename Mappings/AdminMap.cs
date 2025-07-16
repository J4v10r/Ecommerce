using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.AdminDto;

namespace Saas.Mappings
{
    public class AdminMap : Profile{
        public AdminMap()
        {
            CreateMap<AdminCreatDto, Admin>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash));

            CreateMap<Admin, AdminResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
              
        }
    }
}
