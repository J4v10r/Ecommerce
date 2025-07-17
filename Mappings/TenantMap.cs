using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.TenantDto;

namespace Saas.Mappings
{
    public class TenantMap : Profile{
        public TenantMap() {
            CreateMap<Tenant, TenantResponseDto>()
                   .ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.TenantName))
                   .ForMember(dest => dest.TenantEmail, opt => opt.MapFrom(src => src.TenantEmail))
                   .ForMember(dest => dest.TenantPhoneNumber, opt => opt.MapFrom(src => src.TenantPhoneNumber));

            CreateMap<TenantCreatDto, Tenant>()
                .ForMember(dest => dest.TenantId, opt => opt.Ignore())
                .ForMember(dest => dest.TenantName, opt => opt.MapFrom(src => src.TenantName))
                .ForMember(dest => dest.TenantEmail, opt => opt.MapFrom(src => src.TenantEmail))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.TenantPhoneNumber, opt => opt.MapFrom(src => src.TenantPhoneNumber))
                .ForMember(dest => dest.TenantCpf, opt => opt.MapFrom(src => src.TenantCpf));
        }
    }
}
