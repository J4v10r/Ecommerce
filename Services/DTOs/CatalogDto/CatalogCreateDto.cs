using System.ComponentModel.DataAnnotations;

namespace Saas.Services.DTOs.CatalogDto
{
    public class CatalogCreateDto{
        [Required(ErrorMessage = "O título do catálogo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Title { get; set; }
        public AddressCreatDto Address { get; set; }
    }
}
