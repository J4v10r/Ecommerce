using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class User{
        [Key]
        [JsonIgnore]
        public int UserId { get; set; }
        
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do usuário pode ter no máximo 100 caracteres.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um e-mail válido.")]
        [MaxLength(256)]
        public string UserEmail { get; set; }

        [Required]
        [Phone(ErrorMessage = "O Numéro de celular é obrigatório.")]
        [MinLength(8), MaxLength(255)]
        public string UserPhone { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
        public string UserCpf { get; set; }

    }
}
