using Saas.Services.DTOs.CatalogDto;

namespace Saas.Services.CatalogService
{
    public interface ICatalogService
    {
        Task<CatalogResponseDto> AddCatalogAsync(CatalogCreateDto catalogCreateDto, int tenantId);
        Task<bool> DeleteCatalogAsync(int catalogId);
        Task<IEnumerable<CatalogResponseDto>> GetAllCatalogsAsync();
        Task<CatalogResponseDto?> GetCatalogByIdAsync(int catalogId);
        Task<CatalogResponseDto> GetCatalogByTenantIdAsync(int tenantId);
        //Task UpdateCatalogAsync(int catalogId, CatalogUpdateDto catalogUpdateDto);
    }
}
