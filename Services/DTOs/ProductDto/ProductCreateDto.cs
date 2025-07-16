using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.ProductDto
{
    public class ProductCreateDto{

        [Required(ErrorMessage = "O nome do Produto é obrigatório.")]
        [MaxLength(256)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória.")]
        [MaxLength(500)]
        public string ProductDescription { get; set; }

        [MaxLength(2083)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "O produto deve pertencer a um catálogo.")]
        public int CatalogId { get; set; }
    }
}
