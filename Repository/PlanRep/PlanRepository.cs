using System.Numerics;
using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.PlanRep
{
    public class PlanRepository : IPlanRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<PlanRepository> _logger;

        public PlanRepository(AppDbContext context, ILogger<PlanRepository> logger){
            _context = context;
            _logger = logger;
            
        }

        public async Task AddPlanAsync(Plan plan){
            try{
                await _context.AddAsync(plan);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Plano {plan.PlanName} adicionado com sucesso.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar Plano {plan.PlanName} no banco de dados.");
                throw new Exception("Erro ao salvar Plano no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao adicionar o Plano ao banco de dados;");
                throw new Exception("Ocorreu um erro inesperado ao adicionar o  Plano ao banco de dados;", ex);
            }
        }

        public async Task<bool> DeletePlanByIdAsync(int planId)
        {
            try
            {
                var plan = await _context.Plans.FindAsync(planId);

                if (plan == null) 
                {
                    _logger.LogWarning($"Plano com ID {planId} não encontrado.");
                    return false;
                }

                _context.Plans.Remove(plan);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Plano com ID {planId} foi removido com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao remover o plano com ID {planId} no banco de dados.");
                throw new Exception($"Erro ao remover o plano com ID {planId} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao remover o plano com ID {planId}.");
                throw new Exception($"Ocorreu um erro inesperado ao remover o plano com ID {planId}.", ex);
            }
        }

        public async Task<IEnumerable<Plan?>> GetAllPlansAsync(bool includeInactive = false){
            try{
               var plans = await _context.Plans.AsNoTracking().ToListAsync();
                _logger.LogInformation("Planos retornados com sucesso");
                return plans;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de Planos.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de planos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao retornar os planos.");
                throw new Exception($"Ocorreu um erro inesperado ao retornar os planos.", ex);
            }
        }

        public async Task<Plan> GetPlanByIdAsync(int planId)
        {
            try
            {
                var plan = await _context.Plans.AsNoTracking().FirstOrDefaultAsync(p => p.PlanId == planId);
                if (plan == null)
                {
                    _logger.LogWarning($"Plano com ID {planId} não encontrado.");
                }
                _logger.LogInformation($"Plano {planId} retornado com sucesso");
                return plan;

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de Planos.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de planos.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao retornar o plano {planId}.");
                throw new Exception($"Ocorreu um erro inesperado ao retornar o plano {planId}.", ex);
            }
        }

        public async Task<int> GetTenantCountForPlanAsync(int planId)
        {
            try
            {
                int tenantCount = await _context.Tenants.CountAsync(t => t.PlanId == planId);

                _logger.LogInformation($"Encontrados {tenantCount} revendedores para o plano {planId}.");
                return tenantCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao contar os revendedores para o plano {planId}.");
                throw new Exception($"Erro ao contar os revendedores para o plano {planId}.", ex);
            }
        }

        public async Task<bool> UpdatePlanByIdAsync(Plan plan)
        {
            try
            {
                _context.Plans.Update(plan);
                await _context.SaveChangesAsync();  
                return true;  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao tentar atualizar o plano.");
                return false;
            }
        }
    }
}
