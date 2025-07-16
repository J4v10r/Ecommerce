using Saas.Models;
using Saas.Services.DTOs.AdminDto;

namespace Saas.Services.AdminService
{
    public interface IAdminService
    {
        Task<bool> CreateAdminAsync(AdminCreatDto adminCreatDto);
        Task<bool> DeleteAdminAsync(int id);
        public Task<Admin> GetAdminByNameAsync(string name);



    }
}
