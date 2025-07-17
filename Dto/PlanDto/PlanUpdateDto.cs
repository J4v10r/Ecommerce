using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Saas_Sexshop.Dto.PlanDto
{
    public class PlanUpdateDto{
        [StringLength(50, MinimumLength = 3)]
        public string? PlanName { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal MonthlyPrice { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal AnnualPrice { get; set; }

        public int MaxProducts { get; set; }

        public int MaxCatalogs { get; set; }

        public bool AllowCustomDomain { get; set; }

        public bool AllowBrandingRemoval { get; set; }

        public bool Active { get; set; }
    }
}
