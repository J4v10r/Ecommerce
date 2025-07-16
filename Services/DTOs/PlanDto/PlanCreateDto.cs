using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.PlanDto
{
    public class PlanCreateDto{
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string PlanName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MonthlyPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal AnnualPrice { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxProducts { get; set; } = 50;

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxCatalogs { get; set; } = 3;

        public bool AllowCustomDomain { get; set; }

        public bool AllowBrandingRemoval { get; set; } = false;

        public bool Active { get; set; } = true;
    }
}
