using System.ComponentModel.DataAnnotations;

namespace Saas_Sexshop.Dto.CatalogDto
{
    public class CatalogCreateDto{
        [Required(ErrorMessage = "O título do catálogo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O template é obrigatório.")]
        public int TemplateId { get; set; }

        [MaxLength(50, ErrorMessage = "O domínio não pode exceder 50 caracteres.")]
        public string? CustomDomain { get; set; }

        [Required(ErrorMessage = "O tenant (assinante) é obrigatório.")]
        public int TenantId { get; set; } 
    }
}
