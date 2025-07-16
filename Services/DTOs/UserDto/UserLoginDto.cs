using System.ComponentModel.DataAnnotations;
using Saas.Services.DTOs.LoginDto;

namespace Saas.Services.DTOs.UserDto
{
    public class UserLoginDto : ILoginDto
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
