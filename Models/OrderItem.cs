using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class OrderItem
    {
        [Key]
        [JsonIgnore]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser pelo menos 1.")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal PriceAtPurchase { get; set; }

       
        [Column(TypeName = "decimal(5, 2)")]
        [Range(0, 100, ErrorMessage = "O desconto deve estar entre 0 e 100%.")]
        public decimal DiscountPercentage { get; set; } = 0;

        public decimal TotalItemValue =>
            Quantity * (PriceAtPurchase * (1 - DiscountPercentage / 100));

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}