using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(ILogger<CategoryRepository> logger, AppDbContext appDbContext)
        {
            _context = appDbContext;
            _logger = logger;
        }
        public async Task<bool> CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogInformation("Tentativa de passar um parametro nulo ao criar a categoria.");
                    throw new ArgumentNullException(nameof(category));
                }

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Categoria {category.Name} criada com sucesso.");
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorrou um erro inesperado ao adicionar a categoria.");
                throw;
            }

        }

        public async Task<bool> DeleteCategory(int id)
        {
            if (id <= 0)
            {
                _logger.LogInformation("Tentativa de passar um Id menor que 0 ao deletar a categoria.");
                return false;
            }
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    _logger.LogWarning($"Categoria com ID {id} não encontrada para exclusão.");
                    return false;
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Categoria {category.Name} deletada com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao excluir categoria com ID {id}.");
                throw;
            }
        }

        public async Task<Category?> GetCategory(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Tentativa de buscar categoria com ID inválido: {CategoryId}.", id);
                return null;
            }

            try
            {
                var category = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category != null)
                {
                    _logger.LogInformation("Categoria encontrada com ID {CategoryId}.", id);
                }
                else
                {
                    _logger.LogWarning("Categoria com ID {CategoryId} não encontrada.", id);
                }

                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar categoria com ID {CategoryId}.", id);
                throw;
            }
        }

        async Task<IEnumerable<Category?>> ICategoryRepository.GetAllCategories()
        {
            try
            {
                _logger.LogInformation("Buscando todas as categorias no banco de dados.");

                var categories = await _context.Categories
                    .AsNoTracking()
                    .ToListAsync();

                _logger.LogInformation("Busca concluída com sucesso. Total de categorias encontradas: {Count}.", categories.Count);

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todas as categorias.");
                throw;
            }
        }
    }
}
