using System.Threading.Tasks;
using Saas.Models;

namespace Saas.Repository.TenantRep
{
    public interface ITenantRepository{
        Task<bool>AddTenantAsync (Tenant tenant);
        Task<bool>UpdateTenantAsync (int id,Tenant tenant);
        Task<bool> DeleteTenantAsync (int id);
        Task<Tenant?> GetTenantByIdAsync (int id);
        Task<Tenant?> GetTenantByNameAsync (string name);
        Task<Tenant?> GetTenantByPhoneAsync(string phoneNumber);
        Task<Tenant?> GetTenantByEmailAsync(string email);
        Task<Tenant?> GetTenantByCpfAsync(string cpf);
        Task<IEnumerable<Tenant?>> GetAllTenants();
        Task<bool> ChangeTenantPlanAsync(int tenantId, int newPlanId);

    }
}
