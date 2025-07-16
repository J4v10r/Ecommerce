using System.Threading.RateLimiting;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Saas.Models;
using Saas.Repository.TenantRep;
using Saas.Services.DTOs.TenantDto;

namespace Saas.Services.TenantServices
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly ILogger<TenantService> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Tenant> _passwordHasher;
        public TenantService(ITenantRepository tenantRepository, ILogger<TenantService> logger, IMapper mapper, IPasswordHasher<Tenant> passwordHasher)
        {
            _tenantRepository = tenantRepository;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> CreateAsync(TenantCreatDto dto)
        {
            try
            {

                var cpf = await _tenantRepository.GetTenantByCpfAsync(dto.TenantCpf);
                if (cpf != null)
                {
                    _logger.LogWarning($"Esse cpf {dto.TenantCpf} já foi cadastrado!");
                    return false;
                }

                var email = await _tenantRepository.GetTenantByEmailAsync(dto.TenantEmail);
                if (email != null){

                    _logger.LogWarning($"Esse email {dto.TenantEmail} já foi cadastrado!");
                    return false;

                }
                var tenant = _mapper.Map<Tenant>(dto);
                tenant.PasswordHash = _passwordHasher.HashPassword(tenant, dto.PasswordHash);
                
                var result = await _tenantRepository.AddTenantAsync(tenant);
                if (!result)
                {
                    _logger.LogWarning("Falha ao adicionar o Vendedor no banco de dados.");
                    return false;
                }

                _logger.LogInformation($"Vendedor {tenant.TenantName} criado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar Vendedor.");
                return false;
            }
        }

        public async Task<IEnumerable<TenantResponseDto>> GetAllTenantsAsync()
        {
            try
            {
                var tenants = await _tenantRepository.GetAllTenants();
                if (tenants == null || !tenants.Any())
                {
                    _logger.LogWarning("Lista de Vendedores não encontrada ou está vazia.");
                    throw new Exception("Lista de Vendedores não pôde ser retornada.");
                }
                var result = _mapper.Map<IEnumerable<TenantResponseDto>>(tenants);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar o Vendedores.");
                return Enumerable.Empty<TenantResponseDto>(); 
            }
        }

     async Task<TenantResponseDto> ITenantService.GetTenant(int id)
        {
            try
            {
                var result = await _tenantRepository.GetTenantByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning($"Erro ao buscar o Vendedor de id {id}. Vendedor não encontrado.");
                    throw new Exception($"Revendedor com ID {id} não encontrado.");
                }
                _logger.LogInformation($"Vendedor de id {id} retornado com sucesso.");
                return _mapper.Map<TenantResponseDto>(result);

            }
            catch (Exception ex) {
                _logger.LogError($"Erro ao tentar buscar o Vendedor de id {id}");
                throw new Exception("Ocorreu um erro ao buscar o Vendedor");
            }

        }

        async Task<Tenant?> ITenantService.GetTenantByEmailAsync(string email)
        {
            try{
                var tenant = await _tenantRepository.GetTenantByEmailAsync(email);
                if (tenant == null)
                {
                    _logger.LogWarning($"Tenant com o e-mail {email} não foi encontrado.");
                }
                return tenant;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar Tenant com e-mail {email}.");
                throw new Exception($"Erro ao buscar Tenant com e-mail {email}.", ex);
            }
        }


        async Task<bool> ITenantService.RemoveTenantAsync(int id)
        {
            try
            {
                var result = await _tenantRepository.DeleteTenantAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"Falha ao tentar remover o Vendedor com ID {id}.");
                    return false;
                }
                _logger.LogInformation($"Vendedor com ID {id} removido com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o Vendedor.");
                throw new Exception("Erro ao remover o Vendedor. Por favor, tente novamente mais tarde.", ex);
            }


           }
        }
    }
    
