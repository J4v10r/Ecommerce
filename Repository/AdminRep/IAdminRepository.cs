using Saas.Models;

namespace Saas.Repository.AdminRep
{
    public interface IAdminRepository{
        public Task AddAdminAsync(Admin admin);
        public Task DeleteAdminByIdAsync(int id);
        public Task<Admin> GetAdminByNameAsync(string name);

    }
}
