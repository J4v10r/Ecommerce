using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas_Sexshop.Models
{
    public class CatalogCustomization{
        [Key]
        [JsonIgnore]
        public int CatalogCustomizationId { get; set; }

        [ForeignKey("Catalog")]
        [JsonIgnore]
        public int CatalogId { get; set; }

        public virtual Catalog? Catalog { get; set; }

        [MaxLength(100)]
        public string? CatalogName { get; set; }

        [MaxLength(7)]
        public string? PrimaryColor { get; set; }

        [MaxLength(7)]
        public string? SecondaryColor { get; set; }
    }
}
