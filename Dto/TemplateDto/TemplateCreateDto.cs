using System.ComponentModel.DataAnnotations;

namespace Saas_Sexshop.Dto.TemplateDto
{
    public class TemplateCreateDto
    {
        [Required(ErrorMessage = "O nome do template é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "A URL da imagem de prévia deve ter no máximo 255 caracteres.")]
        public string PreviewImageUrl { get; set; } = "/default-preview.jpg";

        [StringLength(50, ErrorMessage = "A chave do frontend deve ter no máximo 50 caracteres.")]
        public string? FrontendKey { get; set; }

        public bool IsActive { get; set; } = true;

        [DataType(DataType.Text)]
        public string? HtmlStructure { get; set; }

        [DataType(DataType.Text)]
        public string? DefaultCss { get; set; }

        public List<string> CustomizableAreas { get; set; } = new List<string>();
    }
}
