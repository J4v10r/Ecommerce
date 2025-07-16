using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.TenantRep
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<TenantRepository> _logger;
        public TenantRepository(AppDbContext appDbContext, ILogger<TenantRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<bool> AddTenantAsync(Tenant tenant)
        {
            try
            {
                await _appDbContext.AddAsync(tenant);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Vendedor {tenant.TenantName} adicionado com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar Vendedor {tenant.TenantName} no banco de dados.");
                return false;         
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado ao adicionar o Vendedor ao banco de dados;");
                return false;
            }
        }

        public async Task<bool> ChangeTenantPlanAsync(int tenantId, int newPlanId)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.FindAsync(tenantId);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com ID {tenantId} não encontrado no banco de dados.");
                    return false;
                }
                var planExists = await _appDbContext.Plans.AnyAsync(p => p.PlanId == newPlanId);
                if (!planExists)
                {
                    _logger.LogWarning($"Plano com ID {newPlanId} não encontrado.");
                    return false;
                }

                tenant.PlanId = newPlanId;
                _appDbContext.Tenants.Update(tenant);
                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation($"Plano do Vendedor {tenantId} alterado para o plano {newPlanId} com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o plano do Vendedor {tenantId} para o plano {newPlanId}.");
                throw new Exception("Erro ao atualizar o plano do Vendedor no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao atualizar o plano do Vendedor {tenantId}.");
                throw new Exception("Erro inesperado ao atualizar o plano do Vendedor.", ex);
            }
        }

        public async Task<bool> DeleteTenantAsync(int id)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.FindAsync(id);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com {id} Não encontrado ");
                    return false;
                }
                _appDbContext.Remove(tenant);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Vendedor com ID {id} foi removido com sucesso");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao remover Vendedor com ID {id} no banco de dados.");
                throw new Exception($"Erro ao remover o Vendedor com ID {id} no banco de dados.", ex);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao remover o Vendedor com ID {id} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao remover o Vendedor com ID {id} ao banco de dados;", ex);
            }
        }

        public async Task<IEnumerable<Tenant?>> GetAllTenants()
        {
            try
            {
                var tenants = await _appDbContext.Tenants.AsNoTracking().ToListAsync();
                return tenants;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a lista de Vendedores.");
                throw new Exception("Ocorreu um erro ao tentar obter a lista de Vendedores.", ex);
            }
        }

        public async Task<Tenant?> GetTenantByCpfAsync(string cpf)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.TenantCpf == cpf);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com CPF {cpf} não encontrado.");
                }
                return tenant;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Vendedor com CPF {cpf}: {ex.Message}");
                throw new Exception($"Erro ao buscar Vendedor com CPF {cpf}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao buscar Vendedor com CPF {cpf}.");
                throw new Exception($"Erro inesperado ao buscar Vendedor com CPF {cpf}.", ex);
            }
        }

        public async Task<Tenant?> GetTenantByEmailAsync(string email)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.TenantEmail == email);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com Email {email} não encontrado.");
                }
                return tenant;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Vendedor com Email {email}: {ex.Message}");
                throw new Exception($"Erro ao buscar Vendedor com Email {email}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao buscar Vendedor com Email {email}.");
                throw new Exception($"Erro inesperado ao buscar Vendedor com Email {email}.", ex);
            }
        }

        public async Task<Tenant?> GetTenantByIdAsync(int id)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.AsNoTracking().FirstOrDefaultAsync(t => t.TenantId == id);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com ID {id} não encontrado.");
                }
                return tenant;

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Vendedor com ID {id} no banco de dados.");
                throw new Exception($"Erro ao buscar Vendedor com ID {id} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao buscar o Vendedor com ID {id} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao buscar o Vendedor com ID {id} ao banco de dados;", ex);
            }
        }

        public async Task<Tenant?> GetTenantByNameAsync(string name)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.FirstOrDefaultAsync(t => t.TenantName == name);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com Nome {name} não encontrado.");
                }
                return tenant;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Vendedor com Nome {name} no banco de dados.");
                throw new Exception($"Erro ao buscar Vendedor com Nome {name} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao buscar o Vendedor com Nome {name} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao buscar o Vendedor com Nome {name} ao banco de dados;", ex);
            }
        }

        public async Task<Tenant?> GetTenantByPhoneAsync(string phoneNumber)
        {
            try
            {
                var tenant = await _appDbContext.Tenants.FirstOrDefaultAsync(t => t.TenantPhoneNumber == phoneNumber);
                if (tenant == null)
                {
                    _logger.LogWarning($"Vendedor com Número {phoneNumber} não encontrado.");
                }
                return tenant;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Vendedor com Número {phoneNumber} no banco de dados.");
                throw new Exception($"Erro ao buscar Vendedor com Número {phoneNumber} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao buscar o Vendedor com Nome {phoneNumber} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao buscar o Vendedor com Nome {phoneNumber} ao banco de dados;", ex);
            }
        }

        public async Task<bool> UpdateTenantAsync(int id, Tenant tenant)
        {
            try
            {
                var existingTenant = await _appDbContext.Tenants.FindAsync(id);
                if (existingTenant == null)
                {
                    _logger.LogWarning($"Vendedor com ID {id} não encontrado.");
                    return false;
                }
                existingTenant.TenantName = tenant.TenantName;
                existingTenant.TenantEmail = tenant.TenantEmail;
                existingTenant.PlanId = tenant.PlanId;

                _appDbContext.Tenants.Update(existingTenant);
                await _appDbContext.SaveChangesAsync();

                _logger.LogInformation($"Vendedor com ID {id} atualizado com sucesso.");
                return true;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o Vendedor com ID {id} no banco de dados.");
                throw new Exception($"Erro ao atualizar o Vendedor com ID {id} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao atualizar o Vendedor com ID {id}.");
                throw new Exception($"Ocorreu um erro inesperado ao atualizar o Vendedor com ID {id}.", ex);
            }
        }
    }
}


