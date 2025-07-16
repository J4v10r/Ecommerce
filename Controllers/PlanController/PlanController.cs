using Microsoft.AspNetCore.Mvc;
using Saas.Models;
using Saas.Repository.PlanRep;
using AutoMapper;
using Saas.Mappings;
using Saas.Services.DTOs;
using Saas.Services.DTOs.TenantDto;
using Saas.Services.PlanServices;
using Saas.Services.DTOs.PlanDto;

namespace Saas.Controllers.PlanController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;
        private readonly IMapper _mapper;
        private readonly ILogger<PlanController> _logger;

        public PlanController(IPlanService planService, IMapper mapper, ILogger<PlanController> logger)
        {
            _planService = planService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PlanCreateDto planCreateDto){
            try{
                var result = await _planService.CreatPlanAsync(planCreateDto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "Não foi possível criar o Plano" });
                }
                _logger.LogInformation("Vendedor criado com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar tenant");
                return StatusCode(500, new { success = false, error = "Erro interno ao criar o Vendedor." });
            }

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _planService.DeletePlanAsync(id);
                _logger.LogInformation($"Plano {id} removido com sucesso.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover Plano {id}");
                throw new Exception("Ocorreu um Erro ao remover o Plano");
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PlanResponseDto>>> GetAll()
        {
            try
            {
                var tenants = await _planService.GetAllPlansAsync();
                return Ok(tenants);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar planos");
                return StatusCode(500, "Erro interno ao processar a requisição");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PlanResponseDto>> Get(int id)
        {
            try
            {
                var result = await _planService.GetPlanByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Plano com ID {id} não encontrado.");
                }
                return Ok(new
                {
                    message = "Plano encontrado com sucesso!",
                    tenant = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return StatusCode(500, "Erro ao buscar o Plano");
            }

        }
    }
}
