using Saas_Sexshop.Dto.OrderItemDto;

namespace Saas_Sexshop.Dto.OrderDto
{
    public class OrderResponseDto{
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = "Pendente";
        public int? OrderPaymentId { get; set; }
        public int TenantId { get; set; }
        public ICollection<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();

    }
}
