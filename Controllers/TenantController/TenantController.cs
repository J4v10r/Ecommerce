using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Saas.Models;
using Saas.Repository.TenantRep;
using Saas.Services.DTOs.TenantDto;
using Saas.Services.TenantServices;

namespace Saas.Controllers.TenantController
{
    
    [Route("api/[controller]")]
    [EnableCors("AllowAngularDev")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly ILogger<TenantController> _logger;
        public TenantController(ITenantService tenantService, ILogger<TenantController> logger)
        {
            _tenantService = tenantService;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Post([FromBody]TenantCreatDto tenantCreatDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _logger.LogInformation("Iniciando criação do Vendedor.");

                var success = await _tenantService.CreateAsync(tenantCreatDto);

                if (!success)
                {
                    return BadRequest(new { success = false, message = "Não foi possível criar o Vendedor" });
                }

                _logger.LogInformation("Vendedor criado com sucesso.");
                return Ok(new { success = true, message = "Vendedor criado com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar tenant");
                return StatusCode(500, new { success = false, error = "Erro interno ao criar o Vendedor." });
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TenantResponseDto>>> GetAll()
        {
            try
            {
                var tenants = await _tenantService.GetAllTenantsAsync();
                return Ok(tenants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar tenants");
                return StatusCode(500, "Erro interno ao processar a requisição");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TenantResponseDto>> Get(int id)
        {
            try
            {
                var result = await _tenantService.GetTenant(id);
                if (result == null) {
                    return NotFound($"Vendedor com ID {id} não encontrado.");
                }
                return Ok(new
                {
                    message = "Vendedor encontrado com sucesso!",
                    tenant = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, "Erro ao buscar o Vendedor");
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id){
            try
            {   
                var result = await _tenantService.RemoveTenantAsync(id);
                _logger.LogInformation($"Vendedor {id} removido com sucesso.");
                return Ok(result);
            }
            catch (Exception ex){
                _logger.LogError(ex,$"Erro ao remover Vendedor {id}");
                throw new Exception("Ocorreu um Erro ao remover o Vendedor");
            }

        }
    }
}
