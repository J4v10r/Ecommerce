using AutoMapper;
using Saas.Services.DTOs.OrderDto;

namespace Saas.Mappings
{
    public class OrderMap : Profile{
        public OrderMap(){
            CreateMap<Order, OrderResponseDto>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.OrderPaymentId, opt => opt.MapFrom(src => src.OrderPaymentId))
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.OrderPaymentId, opt => opt.MapFrom(src => src.OrderPaymentId))
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
        }

    }
}
