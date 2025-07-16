using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class Product{
        [Key]
        [JsonIgnore]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "O nome do Produto é obrigatório.")]
        [MaxLength(256, ErrorMessage = "O nome do produto pode ter no máximo 256 caracteres.")]
        public string? ProductName { get; set; }
        
        [Required(ErrorMessage ="O Preço do produto é obrigatório")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        
        [Required(ErrorMessage ="Você precisa informar a quantidade de produtos em estoque.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity can´t be negative")]
        public int QuantityInStock { get; set; }
       
        [Required(ErrorMessage = "Adicione uma descrição ao produto.")]
        [StringLength(500)]
        public string? ProductDescription { get; set; }

        [MaxLength(2083)]
        public string? ImageUrl { get; set; }

        [ForeignKey("Catalog")]
        public int CatalogId { get; set; }
        public virtual Catalog Catalog { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

    }
}
