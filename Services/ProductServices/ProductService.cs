using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Saas.Models;
using Saas.Repository.ProductRep;
using Saas.Services.CatalogService;
using Saas.Services.DTOs.ProductDto;

namespace Saas.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;
        private readonly ICatalogService _catalogService;
        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger, ICatalogService catalogService){
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task AddProductAsync(ProductCreateDto productCreateDto, ClaimsPrincipal tenant)
        {
            try
            {
                var tenantIdClaim = tenant.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == JwtRegisteredClaimNames.Sub);
                if (tenantIdClaim == null || !int.TryParse(tenantIdClaim.Value, out int tenantId))
                throw new UnauthorizedAccessException("Identificação do usuário inválida.");

                var product = _mapper.Map<Product>(productCreateDto);
                var catalog = await _catalogService.GetCatalogByTenantIdAsync(tenantId);
                if (catalog == null)
                    throw new Exception("Catálogo não encontrado.");
                product.CatalogId = catalog.CatalogId;
                _logger.LogInformation("Produto adicionado com sucesso.");
                await _productRepository.AddProductAsync(product);
            }
            catch (Exception ex){
                _logger.LogError(ex,"Erro ao adicionar Produto.");
                throw;
            }
      
        }

        public async Task<bool> DeleProductByIdAsync(int id, ClaimsPrincipal user)
        {
            var tenantIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == JwtRegisteredClaimNames.Sub);
            if (tenantIdClaim == null || !int.TryParse(tenantIdClaim.Value, out int tenantId))
                throw new UnauthorizedAccessException("Identificação do usuário inválida.");

            var catalog = await _catalogService.GetCatalogByTenantIdAsync(tenantId);
            if (catalog == null)
                throw new Exception("Catálogo não encontrado.");

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
                return false;

            if (product.CatalogId != catalog.CatalogId)
                throw new UnauthorizedAccessException("Você não tem permissão para deletar este produto.");

            await _productRepository.DeleteProductByIdAsync(id);
            _logger.LogInformation("Produto excluído com sucesso");
            return true;
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

        public async Task<IEnumerable<ProductResponseDto>> GetProductsByCatalogIdAsync(ClaimsPrincipal user)
        {
            var tenantIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == JwtRegisteredClaimNames.Sub);
            if (tenantIdClaim == null || !int.TryParse(tenantIdClaim.Value, out int tenantId))
                throw new UnauthorizedAccessException("Identificação do usuário inválida.");

            var catalog = await _catalogService.GetCatalogByTenantIdAsync(tenantId);
            if (catalog == null)
                throw new Exception("Catálogo não encontrado.");

            var products = await _productRepository.GetProductByCatalogAsync(catalog.CatalogId);

            if (products == null || !products.Any())
            {
                _logger.LogWarning("Nenhum produto encontrado para o catálogo ID {CatalogId}.", catalog.CatalogId);
                return Enumerable.Empty<ProductResponseDto>();
            }

            var productDtos = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
            _logger.LogInformation("Produtos retornados com sucesso para o catálogo ID {CatalogId}.", catalog.CatalogId);
            return productDtos;
        }

        public Task<bool> UpdateProductByIdAsync(int id, ProductCreateDto updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}