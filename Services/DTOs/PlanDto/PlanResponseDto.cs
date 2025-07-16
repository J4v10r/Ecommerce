using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.PlanDto
{
    public class PlanResponseDto
    {
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
        public int MaxProducts { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MaxCatalogs { get; set; }

        public bool AllowCustomDomain { get; set; }

        public bool AllowBrandingRemoval { get; set; }

        public bool Active { get; set; }
    }
}
