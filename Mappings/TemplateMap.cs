using AutoMapper;
using Saas_Sexshop.Dto.TemplateDto;
using Saas_Sexshop.Models;

namespace Saas_Sexshop.Mappings
{
    public class TemplateMap : Profile
    {
        public TemplateMap()
        {
            CreateMap<Template, TemplateResponseDto>()
                .ForMember(dest => dest.HtmlStructure, opt => opt.MapFrom(src => src.HtmlStructure))
                .ForMember(dest => dest.DefaultCss, opt => opt.MapFrom(src => src.DefaultCss));


            CreateMap<TemplateCreateDto, Template>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PreviewImageUrl, opt => opt.MapFrom(src => src.PreviewImageUrl))
                .ForMember(dest => dest.FrontendKey, opt => opt.MapFrom(src => src.FrontendKey))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.HtmlStructure, opt => opt.MapFrom(src => src.HtmlStructure))
                .ForMember(dest => dest.DefaultCss, opt => opt.MapFrom(src => src.DefaultCss))
                .ForMember(dest => dest.CustomizableAreas, opt => opt.MapFrom(src => src.CustomizableAreas));


        }
    }
}
