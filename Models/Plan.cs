using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class Plan{
        [Key]
        [JsonIgnore]
        public int PlanId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string PlanName { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MonthlyPrice{ get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal AnnualPrice { get; set; }
        [Required]
        public int MaxProducts { get; set; } = 50;

        [Required]
        public int MaxCatalogs { get; set; } = 3;
        
        public bool AllowCustomDomain { get; set; }

        [Required]
        public bool AllowBrandingRemoval { get; set; } = false;

        public bool Active { get; set; } = true;

        public virtual ICollection<Tenant> Tenants { get; set; }
        public Plan()
        {
            Tenants = new List<Tenant>();
        }
    }
}
