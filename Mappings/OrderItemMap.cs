using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.OrderItemDto;

namespace Saas.Mappings
{
    public class OrderItemMap : Profile{
        public OrderItemMap(){
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.PriceAtPurchase, opt => opt.MapFrom(src => src.PriceAtPurchase))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.TotalItemValue, opt => opt.MapFrom(src => src.TotalItemValue))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom<DateTime>(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.Quantity,opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.PriceAtPurchase, opt => opt.MapFrom(src => src.PriceAtPurchase))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Status,opt => opt.MapFrom(src => src.Status));
        }
    }
}
