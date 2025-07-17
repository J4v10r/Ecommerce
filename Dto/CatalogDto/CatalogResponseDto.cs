using System.Text.Json.Serialization;
using Saas_Sexshop.Models;

namespace Saas_Sexshop.Dto.CatalogDto
{
    public class CatalogResponseDto{
        public int CatalogId { get; set; }
        public string Title { get; set; }
        public int TemplateId { get; set; }
        public string? CustomDomain { get; set; } 
        public int TenantId { get; set; }

        [JsonIgnore] 
        public Tenant? Tenant { get; set; }
    }
}
