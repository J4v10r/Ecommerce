using System.ComponentModel.DataAnnotations;

namespace Saas_Sexshop.Dto.UserDto
{
    public class UserCreateDto{
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(8)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatório")]
        [Phone]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        [StringLength(11)]
        public string UserCpf { get; set; }

        [Required(ErrorMessage = "Catálogo vinculado é obrigatório")]
        public int CatalogId { get; set; }
    }
}
