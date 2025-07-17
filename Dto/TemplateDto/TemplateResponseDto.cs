using System.ComponentModel.DataAnnotations;

namespace Saas_Sexshop.Dto.TemplateDto
{
    public class TemplateResponseDto
    {
    public string? HtmlStructure { get; set; }

    [DataType(DataType.Text)]
    public string? DefaultCss { get; set; }

    }
}
