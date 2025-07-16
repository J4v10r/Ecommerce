using Saas.Models;

namespace Saas.Repository.PlanRep
{
    public interface IPlanRepository{
        Task AddPlanAsync(Plan plan);
        Task<bool> UpdatePlanByIdAsync(Plan plan);
        Task<bool> DeletePlanByIdAsync(int planId);
        Task<Plan> GetPlanByIdAsync(int planId);
        Task<IEnumerable<Plan?>> GetAllPlansAsync(bool includeInactive = false);
        Task<int> GetTenantCountForPlanAsync(int planId);
    }
}
