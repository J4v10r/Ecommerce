using Saas.Models;

namespace Saas.Repository.CatalogRep
{
    public interface ICatalogRepository
    {
        Task AddCatalogAsync(Catalog catalog);
        Task UpdateCatalogAsync(Catalog catalog);
        Task<bool> DeleteCatalogByIdAsync(int catalogId);
        Task<IEnumerable<Catalog>> GetAllCatalogsAsync();
        Task<IEnumerable<Catalog>> GetCatalogsByTenantIdAsync(int tenantId);
    }
}
