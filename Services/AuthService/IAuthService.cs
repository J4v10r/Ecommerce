using Saas_Sexshop.Dto.TenantDto;

namespace Saas_Sexshop.Services.AuthService
{
    public interface IAuthService
    {
        Task<string?> AuthTenant(string email, string password);
    }
}
