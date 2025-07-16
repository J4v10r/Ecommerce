using Saas.Models;

namespace Saas.Repository.CategoryRepository
{
    public interface ICategoryRepository
    {
        Task<bool> CreateCategory(Category category);
        Task<Category?> GetCategory(int id);
        Task<bool> DeleteCategory(int id);
        Task<IEnumerable<Category?>> GetAllCategories();
    }
}
