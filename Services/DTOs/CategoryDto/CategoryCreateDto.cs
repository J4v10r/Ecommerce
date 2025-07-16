using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.CategoryDto
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "O Nome da categoria é obrigatório")]
        [MaxLength(100)]
        public string? Name { get; set; }

    }
}
