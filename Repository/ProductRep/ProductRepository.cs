using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.ProductRep
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Produto {product.ProductName} adicionado com sucesso");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, $"Erro ao adicionar Produto {product.ProductName} no banco de dados.");
                throw new Exception("Erro ao salvar Produto no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao adicionar o produto ao banco de dados;");
                throw new Exception("Ocorreu um erro inesperado ao adicionar o produto ao banco de dados;", ex);
            }
        }
        public async Task<bool> DeleteProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
                if (product == null)
                {
                    _logger.LogWarning($"Produto com Id {id} não encontrado ");
                    return false;
                }
                _context.Remove(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Produto com ID {id} foi removido com sucesso");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao remover Produto com ID {id} no banco de dados.");
                throw new Exception($"Erro ao remover o Produto com ID {id} no banco de dados.", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao remover o Produto com ID {id} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao remover o Produto com ID {id} ao banco de dados;", ex);
            }
        }

        public async Task<bool> DeleteProductByNameAsync(int tenantId, string productName)
        {
            try
            {
                var product = await _context.Products.Include(p => p.Catalog)
                    .FirstOrDefaultAsync(p => p.ProductName == productName && p.Catalog.TenantId == tenantId);

                if (product == null)
                {
                    _logger.LogWarning($"Tentativa de exclusão falhou: Produto '{productName}' não encontrado ou não pertence ao Tenant ID {tenantId}.");
                    return false;
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Produto '{productName}' removido com sucesso pelo Tenant ID {tenantId}");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao remover produto '{productName}' para o Tenant ID {tenantId}.");
                throw new Exception($"Erro ao remover o Produto '{productName}'.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao remover o produto '{productName}' para o Tenant ID {tenantId}");
                throw new Exception($"Erro inesperado ao remover o produto '{productName}' para o Tenant ID {tenantId}", ex);
            }
        }

        public async Task<IEnumerable<Product?>> GetAllProductsAsync()
        {
            try
            {
                var products = await _context.Products.AsNoTracking().ToListAsync();
                _logger.LogInformation($"Produtos retornados com sucesso.");
                return products;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de produtos.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de produtos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao retornar os produtos");
                throw new Exception($"Erro inesperado ao retornar os produtos", ex);
            }
        }

        public async Task<IEnumerable<Product?>> GetAllProductsByPriceAsync(decimal price)
        {
            try
            {
                var products = await _context.Products.Where(p => p.Price <= price).AsNoTracking().ToListAsync();
                _logger.LogInformation($"Produtos retornados com sucesso.");
                return products;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de produtos por preço.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de produtos por preço.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao obter a lista de produtos por preço.");
                throw new Exception($"Erro inesperado ao obter os produtos pelo preço");
            }
        }

        public async Task<IEnumerable<Product?>> GetProductByCatalogAsync(int catalogId)

        {
            try
            {
                var products = await _context.Products.Where(p => p.CatalogId == catalogId).AsNoTracking().ToListAsync();

                if (!products.Any())
                {
                    _logger.LogWarning($"Nenhum produto encontrado para o catálogo de ID {catalogId}.");
                }

                _logger.LogInformation($"Produtos para o catálogo de ID {catalogId} retornados com sucesso.");
                return products;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao obter produtos pelo catálogo.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de produtos para o catálogo.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao retornar os produtos do catálogo de ID {catalogId}.");
                throw new Exception($"Erro inesperado ao retornar os produtos do catálogo de ID {catalogId}.", ex);
            }
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == productId);
                if (product == null)
                {
                    _logger.LogWarning($"Produto com Id {productId} não encontrado ");
                }
                _logger.LogInformation($"Produto {productId} retornado com sucesso.");
                return product;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao buscar o produto pelo ID.");
                throw new Exception("Ocorreu um erro ao tentar buscar o produto pelo ID.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao retornar o Produto com ID {productId} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao retornar o Produto com ID {productId} ao banco de dados;", ex);
            }
        }
        public async Task<Product?> GetProductByNameAsync(string productName)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName);
                if (product == null)
                {
                    _logger.LogWarning($"Não foi possivel encontrar o produto {productName}");
                }
                _logger.LogInformation($"Produto {productName} retornado com sucesso.");
                return product;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao buscar o produto pelo Nome.");
                throw new Exception("Ocorreu um erro ao tentar buscar o produto pelo Nome.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao obter a lista de produtos por preço.");
                throw new Exception($"Erro inesperado ao obter os produtos pelo preço");
            }
        }
        public async Task<IEnumerable<Product?>> GetProductByPriceAsync(decimal price)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => Math.Abs(p.Price - price) < 0.01m)
                    .ToListAsync();

                _logger.LogInformation("Foram encontrados {Count} produtos com preço próximo de {Price}", products.Count, price);

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produtos com preço {Price}", price);
                throw;
            }
        }

        public async Task<bool> UpdateProductByIdAsync(int id, Product updatedProduct)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
                if (product == null)
                {
                    _logger.LogWarning($"Produto com ID {id} não encontrado.");
                    return false;
                }

                product.ProductName = updatedProduct.ProductName;
                product.Price = updatedProduct.Price;
                product.ProductDescription = updatedProduct.ProductDescription;
                product.QuantityInStock = updatedProduct.QuantityInStock;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Produto com ID {id} atualizado com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o produto com ID {id}.");
                throw new Exception($"Erro ao atualizar o produto com ID {id}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao atualizar o produto com ID {id}.");
                throw new Exception($"Erro inesperado ao atualizar o produto com ID {id}.", ex);
            }
        }



        public async Task<bool> UpdateProductByNameAsync(string productName, Product updatedProduct)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productName);
                if (product == null)
                {
                    _logger.LogWarning($"Produto com nome '{productName}' não encontrado.");
                    return false;
                }

                product.Price = updatedProduct.Price;
                product.ProductDescription = updatedProduct.ProductDescription;
                product.QuantityInStock = updatedProduct.QuantityInStock;

                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Produto '{productName}' atualizado com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o produto '{productName}'.");
                throw new Exception($"Erro ao atualizar o produto '{productName}'.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao atualizar o produto '{productName}'.");
                throw new Exception($"Erro inesperado ao atualizar o produto '{productName}'.", ex);
            }
        }
    }
}
