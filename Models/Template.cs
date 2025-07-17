using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Saas_Sexshop.Models
    {
        public class Template
        {
            [Key]
            [JsonIgnore]
            public int TemplateId { get; set; }

            [Required]
            [StringLength(100)]
            public string Name { get; set; }

            [Required]
            [StringLength(255)]
            public string PreviewImageUrl { get; set; } = "/default-preview.jpg";

            [Required]
            [StringLength(50)]
            public string? FrontendKey { get; set; }
            public bool IsActive { get; set; } = true;
     
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            
            [Column(TypeName = "text")]
            public string? HtmlStructure { get; set; }

            [Column(TypeName = "text")]
            public string? DefaultCss { get; set; } 

            [Column(TypeName = "json")]
            public List<string> CustomizableAreas { get; set; } = new List<string>();
        }
    }


