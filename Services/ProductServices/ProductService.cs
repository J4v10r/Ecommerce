using AutoMapper;
using Saas.Models;
using Saas.Repository.ProductRep;
using Saas.Services.DTOs.ProductDto;

namespace Saas.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger){
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddProductAsync(ProductCreateDto productCreateDto){
            try
            {
                var product = _mapper.Map<Product>(productCreateDto);
                _logger.LogInformation("Produto adicionado com sucesso.");
                await _productRepository.AddProductAsync(product);
            }
            catch (Exception ex){
                _logger.LogError(ex,"Erro ao adicionar Produto.");
                throw;
            }
      
        }

        public async Task<bool> DeleProductByIdAsync(int id)
        {
            try
            {
                if (id < 0){
                    throw new Exception("O id deve ser maior que zero.");
                    return false;
                }
                await _productRepository.DeleteProductByIdAsync(id);
                _logger.LogInformation("produto excluido com sucesso");
                return true;
            }
            catch(Exception ex){
                _logger.LogError(ex, "Erro ao adicionar Produto.");
                throw;
            }
        }


        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();
                var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
                return result;
            }
            catch (Exception ex){
                _logger.LogError(ex,"Erro ao pegar lista de produtos");
                throw;
            }
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsByPriceAsync(decimal price)
        {
            try
            {
                if (price < 0)
                {
                    throw new ArgumentException("O preço não pode ser negativo", nameof(price));
                }

                var products = await _productRepository.GetProductByPriceAsync(price);
                if (products == null || !products.Any())
                {
                    return Enumerable.Empty<ProductResponseDto>();
                }
                return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao retornar produtos pelo preço {Price}", price);
                throw;
            }
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(int id)
        {
            try
            {
                if (id < 0)
                {
                    throw new ArgumentException("O preço não pode ser negativo");
                }
                var product = await _productRepository.GetProductByIdAsync(id);
                var result = _mapper.Map<ProductResponseDto>(product);
                return result;
            }
            catch (Exception ex){
                _logger.LogError(ex,"Erro ao retornar produto pelo id.");
                throw;
            }
        }

        public Task<bool> UpdateProductByIdAsync(int id, ProductCreateDto updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}
