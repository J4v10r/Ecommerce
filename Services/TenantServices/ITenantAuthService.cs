using Saas.Services.DTOs.TenantDto;

namespace Saas.Services.TenantServices
{
    public interface ITenantAuthService
    {
        Task<string?> Authenticate(TenantLoginDto tenantLoginDto);
    }
}
