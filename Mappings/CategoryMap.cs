using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.CategoryDto;

namespace Saas.Mappings
{
    public class CategoryMap : Profile
    {
        public CategoryMap() {
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Category, CategoryResponseDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
