using System.ComponentModel.DataAnnotations;

namespace Saas.Models
{
    public class Address{
        [Key]
        public int AdressId { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Neighborhood { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public string? Number { get; set; }
        [Required]
        public string? PostalCode { get; set; }
    }
}
