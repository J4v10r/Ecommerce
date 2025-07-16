using AutoMapper;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

using Saas.Models;
using Saas.Services.DTOs.PlanDto;

namespace Saas.Mappings
{
    public class PlanMap : Profile
    {
        public PlanMap()
        {
            CreateMap<PlanCreateDto, Plan>()
                .ForMember(dest => dest.MonthlyPrice, opt => opt.MapFrom(src => src.MonthlyPrice)) 
                .ForMember(dest => dest.AnnualPrice, opt => opt.MapFrom(src => src.AnnualPrice))
                .ForMember(dest => dest.MaxProducts, opt => opt.MapFrom(src => src.MaxProducts))
                .ForMember(dest => dest.MaxCatalogs, opt => opt.MapFrom(src => src.MaxCatalogs))
                .ForMember(dest => dest.AllowCustomDomain, opt => opt.MapFrom(src => src.AllowCustomDomain))
                .ForMember(dest => dest.AllowBrandingRemoval, opt => opt.MapFrom(src => src.AllowBrandingRemoval))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            CreateMap<PlanUpdateDto, Plan>()
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.PlanName))
                .ForMember(dest => dest.MonthlyPrice, opt => opt.MapFrom(src => src.MonthlyPrice))
                .ForMember(dest => dest.AnnualPrice, opt => opt.MapFrom(src => src.AnnualPrice))
                .ForMember(dest => dest.MaxProducts, opt => opt.MapFrom(src => src.MaxProducts))
                .ForMember(dest => dest.MaxCatalogs, opt => opt.MapFrom(src => src.MaxCatalogs))
                .ForMember(dest => dest.AllowCustomDomain, opt => opt.MapFrom(src => src.AllowCustomDomain))
                .ForMember(dest => dest.AllowBrandingRemoval, opt => opt.MapFrom(src => src.AllowBrandingRemoval))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

            CreateMap<Plan, PlanResponseDto>()
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.PlanName))
                .ForMember(dest => dest.MonthlyPrice, opt => opt.MapFrom(src => src.MonthlyPrice))
                .ForMember(dest => dest.AnnualPrice, opt => opt.MapFrom(src => src.AnnualPrice))
                .ForMember(dest => dest.MaxProducts, opt => opt.MapFrom(src => src.MaxProducts))
                .ForMember(dest => dest.MaxCatalogs, opt => opt.MapFrom(src => src.MaxCatalogs))
                .ForMember(dest => dest.AllowCustomDomain, opt => opt.MapFrom(src => src.AllowCustomDomain))
                .ForMember(dest => dest.AllowBrandingRemoval, opt => opt.MapFrom(src => src.AllowBrandingRemoval))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Active));

        }
    }
};


