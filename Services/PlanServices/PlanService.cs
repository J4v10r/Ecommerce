using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using AutoMapper;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Crypto.Utilities;
using Saas.Services.DTOs.TenantDto;
using Saas.Models;
using Saas.Repository.PlanRep;
using Saas.Services.DTOs.PlanDto;

namespace Saas.Services.PlanServices
{
    public class PlanService : IPlanService
    {
        private readonly ILogger<PlanService> _logger;
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository, IMapper mapper, ILogger<PlanService> logger){
            _planRepository = planRepository;
            _mapper = mapper; 
            _logger = logger;
        }



        public async Task<bool> CreatPlanAsync(PlanCreateDto planCreateDto)
        {
            try{
                var plan = _mapper.Map<Plan>(planCreateDto);
  
                await _planRepository.AddPlanAsync(plan);
                _logger.LogInformation("Plano criado com sucesso");
                return true;
            }
            catch (SqlTypeException ex)
            {
                _logger.LogError(ex, "Erro ao acessar o banco de dados ao criar o plano.");
                return false;
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Erro de validação ao criar o plano.");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar criar o plano. Detalhes: {PlanDetails}", planCreateDto);
                return false;
            }
        }

        public async Task<bool> DeletePlanAsync(int id)
        {
            try{
                var result = await _planRepository.DeletePlanByIdAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"Falha ao tentar remover o Plano com ID {id}.");
                    return false;
                }
                _logger.LogInformation($"Plano com ID {id} removido com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o Plano.");
                throw new Exception("Erro ao remover o Plano. Por favor, tente novamente mais tarde.", ex);
            }
        }

        public async Task<IEnumerable<PlanResponseDto>> GetAllPlansAsync(){
            try
            {
                var plans = await _planRepository.GetAllPlansAsync();
                if (plans == null || !plans.Any())
                {
                    _logger.LogWarning("Lista de Planos não encontrada ou está vazia.");
                    throw new Exception("Lista de Planos não pôde ser retornada.");
                }
                var result = _mapper.Map<IEnumerable<PlanResponseDto>>(plans);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o Plano.");
                return Enumerable.Empty<PlanResponseDto>();
            }

        }

        public async Task<bool> UpdatePlanAsync(int id, PlanUpdateDto updatedPlan)
        {
            try
            {
                var plan = await _planRepository.GetPlanByIdAsync(id);
                if (plan == null)
                {
                    _logger.LogWarning($"Plano com ID {id} não encontrado.");
                    return false;
                }

                _mapper.Map(updatedPlan, plan);

                var result = await _planRepository.UpdatePlanByIdAsync(plan);
                if (!result)
                {
                    _logger.LogWarning($"Falha ao tentar atualizar o Plano com ID {id}.");
                    return false;
                }

                _logger.LogInformation($"Plano com ID {id} atualizado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o Plano.");
                return false;
            }
        }

        async Task<PlanResponseDto> IPlanService.GetPlanByIdAsync(int id)
        {
            try{
                var result = await _planRepository.GetPlanByIdAsync(id);

                if (result == null)
                {
                    _logger.LogWarning($"Erro ao buscar o Plano de id {id}. plano não encontrado.");
                    throw new Exception($"Plano com ID {id} não encontrado.");
                }
                _logger.LogInformation($"Plano de id {id} retornado com sucesso.");
                return _mapper.Map<PlanResponseDto>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao tentar buscar o Plano de id {id}");
                throw new Exception("Ocorreu um erro ao buscar o Plano");
            }
        
        }
    }
}
