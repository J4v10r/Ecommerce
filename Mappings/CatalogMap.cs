using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.CatalogDto;

namespace Saas.Mappings
{
    public class CatalogMap : Profile{
        public CatalogMap()
        {
            CreateMap<CatalogCreateDto, Catalog>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId));

            CreateMap<Catalog, CatalogResponseDto>()
                .ForMember(dest => dest.CatalogId, opt => opt.MapFrom(src => src.CatalogId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId));
        }
    }
}
