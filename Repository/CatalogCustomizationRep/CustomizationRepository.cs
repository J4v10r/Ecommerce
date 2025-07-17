using Microsoft.EntityFrameworkCore;
using Saas_Sexshop.Infrastructure.Context;
using Saas_Sexshop.Models;

namespace Saas_Sexshop.Repository.CatalogCustomizationRep
{
    public class CustomizationRepository : ICustomizationRepository
    {
        private readonly AppDbContext _context;

        private readonly ILogger<CustomizationRepository> _logger;

        public CustomizationRepository(AppDbContext context, ILogger<CustomizationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CatalogCustomization?> GetByCatalogIdAsync(int catalogId)
        {
            try
            {
                return await _context.CatalogCustomizations
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CatalogId == catalogId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar customização do catálogo ID {CatalogId}", catalogId);
                throw;
            }
        }

        public async Task AddAsync(CatalogCustomization customization)
        {
            try
            {
                await _context.CatalogCustomizations.AddAsync(customization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar customização para o catálogo ID {CatalogId}", customization.CatalogId);
                throw;
            }
        }

        public async Task UpdateAsync(CatalogCustomization customization)
        {
            try
            {
                _context.CatalogCustomizations.Update(customization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar customização do catálogo ID {CatalogId}", customization.CatalogId);
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar alterações no banco de dados.");
                throw;
            }
        }
    }
}
