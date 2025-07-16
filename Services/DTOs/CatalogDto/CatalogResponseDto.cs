using System.Text.Json.Serialization;
using Saas.Models;

namespace Saas.Services.DTOs.CatalogDto
{
    public class CatalogResponseDto{
        public int CatalogId { get; set; }
        public string Title { get; set; }
        public int TenantId { get; set; }

        [JsonIgnore] 
        public Tenant? Tenant { get; set; }
    }
}
