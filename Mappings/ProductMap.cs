using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.ProductDto;
namespace Saas.Mappings
{
    public class ProductMap : Profile{
        public ProductMap(){

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.QuantityInStock,opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(des => des.ProductDescription, opt => opt.MapFrom(src=>src.ProductDescription))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.CatalogId, opt => opt.MapFrom(src => src.CatalogId));


            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
                .ForMember(des => des.ProductDescription, opt => opt.MapFrom(src => src.ProductDescription))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        }

    }
}
