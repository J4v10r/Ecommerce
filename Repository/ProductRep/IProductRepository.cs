using System.Threading.Tasks;
using Saas.Models;
using System.Collections.Generic;

namespace Saas.Repository.ProductRep
{
    public interface IProductRepository{
        Task AddProductAsync(Product product );
        Task<bool> UpdateProductByIdAsync(int id, Product updatedProduct);
        Task<bool> UpdateProductByNameAsync(string productName, Product updatedProduct);

        Task<bool> DeleteProductByIdAsync(int id);
        Task<bool> DeleteProductByNameAsync(int tenantId, string productName);

        Task<Product?> GetProductByIdAsync(int productId);
        Task<Product?> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product?>> GetProductByPriceAsync(decimal price);
        Task<IEnumerable<Product?>> GetAllProductsByPriceAsync(decimal price);

        Task<IEnumerable<Product?>> GetAllProductsAsync();
        Task<IEnumerable<Product?>> GetProductByCatalogAsync(int catalogId);
    }
}
