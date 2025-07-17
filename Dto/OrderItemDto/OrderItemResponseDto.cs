using Saas_Sexshop.Models;

namespace Saas_Sexshop.Dto.OrderItemDto
{
    public class OrderItemResponseDto{
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal TotalItemValue { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Product Product { get; set; }
    }
}
