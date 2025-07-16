using AutoMapper;
using Saas.Models;
using Saas.Repository.CatalogRep;
using Saas.Services.DTOs.CatalogDto;

namespace Saas.Services.CatalogService
{
    public class CatalogService: ICatalogService{
        private readonly ICatalogRepository _catalogRepository;
        private readonly ILogger<CatalogService> _logger;
        private readonly IMapper _mapper;

        public CatalogService(ICatalogRepository catalogRepository, ILogger<CatalogService> logger, IMapper mapper){
            _catalogRepository = catalogRepository;
            _logger = logger;   
            _mapper = mapper;
        }

        public async Task<CatalogResponseDto> AddCatalogAsync(CatalogCreateDto catalogCreateDto)
        {
            try
            {
                var catalog = _mapper.Map<Catalog>(catalogCreateDto);
                await _catalogRepository.AddCatalogAsync(catalog);
                return _mapper.Map<CatalogResponseDto>(catalog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar catálogo.");
                throw;
            }
        }

        public async Task<bool> DeleteCatalogAsync(int catalogId)
        {
            try
            {
                return await _catalogRepository.DeleteCatalogByIdAsync(catalogId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar catálogo com ID: {catalogId}.");
                throw;
            }
        }

        public async Task<IEnumerable<CatalogResponseDto>> GetAllCatalogsAsync()
        {
            try
            {
                var catalogs = await _catalogRepository.GetAllCatalogsAsync();
                return _mapper.Map<IEnumerable<CatalogResponseDto>>(catalogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os catálogos.");
                throw;
            }
        }

        public async Task<CatalogResponseDto?> GetCatalogByIdAsync(int catalogId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<CatalogResponseDto>> GetCatalogsByTenantIdAsync(int tenantId)
        {
            try
            {
                var catalogs = await _catalogRepository.GetCatalogsByTenantIdAsync(tenantId);
                return _mapper.Map<IEnumerable<CatalogResponseDto>>(catalogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter catálogos com Tenant ID: {tenantId}.");
                throw;
            }
        }
    }
}
