using System.ComponentModel.DataAnnotations;

namespace Saas_Sexshop.Dto.AdminDto
{
    public class AdminCreatDto{
        [Required(ErrorMessage = "O nome do administrador é obrigatório.")]

        public required string Name { get; set; }

        [Required(ErrorMessage = "A senha do admin deve ser fornecida.")]
        public required string PasswordHash { get; set; }
    }
}
