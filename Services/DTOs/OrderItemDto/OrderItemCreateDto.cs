using MercadoPago;
using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.OrderItemDto
{
    public class OrderItemCreateDto{
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal PriceAtPurchase { get; set; }

        [Range(0, 100, ErrorMessage = "O desconto deve estar entre 0 e 100%.")]
        public decimal DiscountPercentage { get; set; } = 0;

        [Required]
        public int ProductId { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
