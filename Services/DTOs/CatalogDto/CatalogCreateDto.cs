using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.CatalogDto
{
    public class CatalogCreateDto{
        [Required(ErrorMessage = "O título do catálogo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O tenant (assinante) é obrigatório.")]
        public int TenantId { get; set; } 
    }
}
