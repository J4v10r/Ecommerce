using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Crypto.Generators;



namespace Saas.Models
{
    public class Tenant
    {

        [Key]
        [JsonIgnore]
        public int TenantId { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome do usuário pode ter no máximo 100 caracteres.")]
        
        public string TenantName { get; set; }
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Por favor, insira um e-mail válido.")]
        [MaxLength(256)]
        public string TenantEmail { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [JsonIgnore]
        public string PasswordHash { get; set; }
        
        [Required]
        [Phone(ErrorMessage = "O Numéro de celular é obrigatório.")]
        [MinLength(8), MaxLength(255)]
        public string TenantPhoneNumber { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter apenas números.")]
        public string TenantCpf { get; set; }

        [JsonIgnore]
        [ForeignKey("PlanId")]
        public Plan TenantPlan { get; set; }
        public int? PlanId { get; set; }
        [JsonIgnore]
        public ICollection<Catalog> Catalogs { get; set; } = new List<Catalog>();

        public Tenant()
        {
            
        }
    }
}