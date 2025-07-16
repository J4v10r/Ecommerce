using AutoMapper;
using Saas.Models;
using Saas.Services.DTOs.PaymentDto;

namespace Saas.Mappings
{
    public class PaymentMap : Profile
    {
        public PaymentMap()
        {
            CreateMap<PaymentCreatDto, Payment>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
                .ForMember(dest => dest.PaymentStatusId, opt => opt.MapFrom(src => src.PaymentStatusId))
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => src.PlanId))
                .ForMember(dest => dest.GatewayTransactionId, opt => opt.MapFrom(src => src.GatewayTransactionId))
                .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceId))
                .ForMember(dest => dest.InvoiceIssueDate, opt => opt.MapFrom(src => src.InvoiceIssueDate))
                .ForMember(dest => dest.InvoiceUrl, opt => opt.MapFrom(src => src.InvoiceUrl));

            CreateMap<Payment, PaymentResponseDto>()
                 .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
                 .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
                 .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                 .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus))
                 .ForMember(dest => dest.GatewayTransactionId, opt => opt.MapFrom(src => src.GatewayTransactionId))
                 .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceId))
                 .ForMember(dest => dest.InvoiceIssueDate, opt => opt.MapFrom(src => src.InvoiceIssueDate))
                 .ForMember(dest => dest.InvoiceUrl, opt => opt.MapFrom(src => src.InvoiceUrl))
                 .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => (int)src.TenantId))
                 .ForMember(dest => dest.PlanId, opt => opt.MapFrom(src => (int)src.PlanId));
        }
    }
}