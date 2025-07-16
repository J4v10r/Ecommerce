using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas.Models
{
    public class Admin{
        [Key]
        [JsonIgnore]
        public int AdminId { get; set; }

        [Required(ErrorMessage = "O nome do administrador é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do usuário pode ter no máximo 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="A senha do admin deve ser fornecida.")]
        public string PasswordHash { get; set; }

        public Admin() { }

    }
}
