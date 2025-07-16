using Microsoft.AspNetCore.Mvc;
using Saas.Services.CatalogService;
using Saas.Services.DTOs.CatalogDto;

namespace Saas.Controllers.CatalogController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(ICatalogService catalogService, ILogger<CatalogController> logger)
        {
            _catalogService = catalogService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CatalogResponseDto>> Post([FromBody] CatalogCreateDto catalogCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var catalog = await _catalogService.AddCatalogAsync(catalogCreateDto);
                return CreatedAtAction(nameof(GetById), new { id = catalog.CatalogId }, catalog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar catálogo.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao adicionar o catálogo.");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogResponseDto>>> GetAll()
        {
            try
            {
                var catalogs = await _catalogService.GetAllCatalogsAsync();
                if (catalogs == null || !catalogs.Any())
                {
                    return NoContent();
                }
                return Ok(catalogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todos os catálogos.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao obter os catálogos.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CatalogResponseDto>> GetById(int id)
        {
            try
            {
                var catalog = await _catalogService.GetCatalogByIdAsync(id);
                if (catalog == null)
                {
                    return NotFound();
                }
                return Ok(catalog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter catálogo com ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao obter o catálogo.");
            }
        }
      

        [HttpGet("tenant/{tenantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogResponseDto>>> GetByTenantId(int tenantId)
        {
            try
            {
                var catalogs = await _catalogService.GetCatalogsByTenantIdAsync(tenantId);
                if (catalogs == null || !catalogs.Any())
                {
                    return NoContent();
                }
                return Ok(catalogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter catálogos com Tenant ID: {tenantId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao obter os catálogos por Tenant ID.");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _catalogService.DeleteCatalogAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar catálogo com ID: {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno ao deletar o catálogo.");
            }
        }
    }
}