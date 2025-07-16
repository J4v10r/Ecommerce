using Saas.Services.DTOs.AdminDto;

namespace Saas.Services.AdminService
{
    public interface IAdminAuthService
    {
        Task<string?> AuthenticateAsync(AdminLoginDto adminLoginDto);
    }
}
