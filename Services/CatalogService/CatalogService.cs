using AutoMapper;
using Saas.Models;
using Saas.Repository.CatalogRep;
using Saas.Services.DTOs.CatalogDto;

namespace Saas.Services.CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly ILogger<CatalogService> _logger;
        private readonly IMapper _mapper;

        public CatalogService(ICatalogRepository catalogRepository, ILogger<CatalogService> logger, IMapper mapper){
            _catalogRepository = catalogRepository;
            _logger = logger;   
            _mapper = mapper;
        }

        public async Task<CatalogResponseDto> AddCatalogAsync(CatalogCreateDto catalogCreateDto, int tenantId)
        {
            try
            {
                var catalog = _mapper.Map<Catalog>(catalogCreateDto);
                catalog.TenantId = tenantId;
                var address = _mapper.Map<Address>(catalogCreateDto.Address);
                catalog.Address = address;
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
            if (catalogId < 0)
            {
                _logger.LogWarning($"ID de catálogo inválido: {catalogId}");
                return null;
            }

            try
            {
                var catalog = await _catalogRepository.GetCatalogByIdAsync(catalogId);

                if (catalog == null)
                {
                    _logger.LogWarning($"Catálogo com ID {catalogId} não encontrado.");
                    return null;
                }

                _logger.LogInformation($"Catálogo com ID {catalogId} encontrado com sucesso.");

                return _mapper.Map<CatalogResponseDto>(catalog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter catálogo com ID {catalogId}.");
                throw; 
            }
        }

        public async Task<CatalogResponseDto> GetCatalogByTenantIdAsync(int tenantId)
        {
            try
            {
                var catalogs = await _catalogRepository.GetCatalogByTenantIdAsync(tenantId);
                return _mapper.Map<CatalogResponseDto>(catalogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter catálogos com Tenant ID: {tenantId}.");
                throw;
            }
        }

        
    }
}