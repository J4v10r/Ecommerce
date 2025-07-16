using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas.Services.DTOs.TenantDto
{
    public class TenantResponseDto{
        [Key]
        public int TenantId { get; set; }

        [Required]
        [MaxLength(100)]

        public string TenantName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string TenantEmail { get; set; }
        [Required]
        [Phone]
        [MinLength(8), MaxLength(255)]
        public string TenantPhoneNumber { get; set; }
    }
}
