using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.CatalogRep
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CatalogRepository> _logger;

        public CatalogRepository(AppDbContext context, ILogger<CatalogRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddCatalogAsync(Catalog catalog)
        {
            try
            {
                await _context.AddAsync(catalog);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Catalogo com {catalog.CatalogId} adicionado com sucesso");
            }
            catch (DbUpdateException ex){
                _logger.LogError(ex,$"Erro ao adicionar catalogo de id {catalog.CatalogId} no banco de dados.");
                throw new DbUpdateException("Ocorreu um erro ao adicionar o catálogo. Tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao adicionar catálogo de ID {catalog.CatalogId}");
                throw new ApplicationException("Ocorreu um erro inesperado. Tente novamente mais tarde.", ex);
            }
        }

        public async Task<bool> DeleteCatalogByIdAsync(int catalogId)
        {
            try
            {
                var catalog = await _context.Catalogs.FindAsync(catalogId);
                if (catalog == null)
                {
                    _logger.LogWarning($"Catalogo com ID {catalogId} não encontrado.");
                    return false;
                }
                _context.Remove(catalog);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Catalogo com ID  {catalogId} foi removido com sucesso");
                return true;

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,$"Erro ao remover Catalogo com ID  {catalogId} do banco de dados.");
                throw new Exception($"Erro ao remover Catalogo com ID  {catalogId} do banco de dados.", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Erro inesperado ao remover Catalogo com ID  {catalogId} do banco de dados;");
                throw new Exception($"Erro inesperado ao remover Catalogo com ID  {catalogId} do banco de dados;",ex);
            }
        }

        public async Task<IEnumerable<Catalog>> GetAllCatalogsAsync()
        {
            try
            {
                var catalog = await _context.Catalogs.AsNoTracking().ToListAsync();
                _logger.LogInformation($"Todos os pagamentos foram retornados");
                return (IEnumerable<Catalog>)catalog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de catalogos.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de catalogos.", ex);
            }
        }

        public async Task<Catalog?> GetCatalogByIdAsync(int catalogId)
        {
            try
            {
                var catalog = await _context.Catalogs.AsNoTracking().FirstOrDefaultAsync(p => p.CatalogId == catalogId );
                if (catalog == null)
                {
                    _logger.LogWarning($"Catalogo com id {catalogId} não encontrado.");
                }
                _logger.LogInformation($"catalogo de id {catalogId} retornado com sucesso");
                return catalog;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Erro ao obter catalogo por id.");
                throw new Exception("Erro ao buscar catalogo por id.", ex);
            }
        }

        public async Task<IEnumerable<Catalog>> GetCatalogsByTenantIdAsync(int tenantId)
        {
            try
            {
                var catalogs = await _context.Catalogs.AsNoTracking().Where(c => c.TenantId == tenantId).ToListAsync();

                if (!catalogs.Any())
                {
                    _logger.LogWarning("Nenhum catálogo encontrado para o Tenant ID {TenantId}.", tenantId);
                }

                return catalogs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar catálogos por Tenant ID {TenantId}.", tenantId);
                throw new ApplicationException("Erro ao buscar catálogos. Tente novamente mais tarde.", ex);
            }
        }

        public async Task UpdateCatalogAsync(Catalog catalog)
        {
            try
            {
                var existingCatalog = await _context.Catalogs.FindAsync(catalog.CatalogId);
                if (existingCatalog == null)
                {
                    _logger.LogWarning("Tentativa de atualizar um catálogo inexistente. ID: {CatalogId}", catalog.CatalogId);
                    throw new KeyNotFoundException($"Catálogo com ID {catalog.CatalogId} não encontrado.");
                }

                _context.Entry(existingCatalog).CurrentValues.SetValues(catalog);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Catálogo com ID {CatalogId} atualizado com sucesso.", catalog.CatalogId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erro ao atualizar catálogo ID {CatalogId}.", catalog.CatalogId);
                throw new ApplicationException("Erro ao atualizar catálogo. Tente novamente mais tarde.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar catálogo ID {CatalogId}.", catalog.CatalogId);
                throw new ApplicationException("Erro inesperado ao atualizar catálogo.", ex);
            }
        }
    }
}