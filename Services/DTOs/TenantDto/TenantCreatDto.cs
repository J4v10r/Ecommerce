using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas.Services.DTOs.TenantDto
{
    public class TenantCreatDto
    {

        [Required]
        [MaxLength(100)]

        public string TenantName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string TenantEmail { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string PasswordHash { get; set; }

        [Required]
        [Phone]
        [MinLength(8), MaxLength(255)]
        public string TenantPhoneNumber { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        [RegularExpression(@"^\d{11}$")]
        public string TenantCpf { get; set; }

        public int? PlanId { get; set; }
    }
}
