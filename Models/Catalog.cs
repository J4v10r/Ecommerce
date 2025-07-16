using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class Catalog{  
        [Key]
        [JsonIgnore]
        public int CatalogId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Title { get; set; } 
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [ForeignKey("Tenant")]

        public int TenantId { get; set; }

        public Tenant? Tenant { get; set; }

    }
}
