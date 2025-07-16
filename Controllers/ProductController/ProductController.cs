using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas.Services.DTOs.ProductDto;
using Saas.Services.ProductServices;

namespace Saas.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase{

        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
    public ProductController(IProductService productService, ILogger<ProductController>logger){
            _productService = productService;
            _logger = logger;
    }

       [HttpPost]
       [ProducesResponseType(StatusCodes.Status201Created)]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody]ProductCreateDto productCreateDto){
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await _productService.AddProductAsync(productCreateDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex){
                _logger.LogError(ex, "Erro ao adicionar Produto.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao adicionar o produto.");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> Get(){
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex){
                _logger.LogError($"{ex.Message},Erro ao retornar a lista de produtos.");
                return BadRequest();
            }
        }

        [HttpGet("price")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ProductResponseDto>> Get([FromQuery] decimal price){
            try
            {
               var products = await _productService.GetAllProductsByPriceAsync(price);
               return Ok(products);
            }

            catch (Exception ex){
                _logger.LogError(ex,"Erro ao buscar produto pelo preço.");
                throw;  
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductResponseDto>> Get(int id) {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produto pelo preço.");
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _productService.DeleProductByIdAsync(id);
                _logger.LogInformation($"Produto {id} removido com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover Produto {id}");
                throw new Exception("Ocorreu um Erro ao remover o Produto");
            }

        }


    }
}
