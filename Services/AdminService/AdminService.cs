using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Saas.Models;
using Saas.Repository.AdminRep;
using Saas.Repository.TenantRep;
using Saas.Services.DTOs.AdminDto;
using Saas.Services.TenantServices;

namespace Saas.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ILogger<AdminService> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Admin> _passwordHasher;
        public AdminService(IAdminRepository adminRepository, ILogger<AdminService> logger, IMapper mapper, IPasswordHasher<Admin> passwordHasher)
        {
            _adminRepository = adminRepository;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> CreateAdminAsync(AdminCreatDto adminCreatDto)
        {
            try
            {
                var admin =  _mapper.Map<Admin>(adminCreatDto);
                admin.PasswordHash = _passwordHasher.HashPassword(admin, adminCreatDto.PasswordHash);

               _logger.LogInformation("Admin criado com sucesso");
               await _adminRepository.AddAdminAsync(admin);
               return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar Admin.");
                return false;
            }
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            try
            {
                await _adminRepository.DeleteAdminByIdAsync(id);
                _logger.LogInformation($"Plano com ID {id} removido com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover o Plano.");
                throw new Exception("Erro ao remover o Plano. Por favor, tente novamente mais tarde.", ex);
            }
        }
        public async Task<Admin> GetAdminByNameAsync(string name)
        {
            try
            {
                var admin = await _adminRepository.GetAdminByNameAsync(name);
                if (admin == null)
                {
                    _logger.LogWarning("Admin com o nome '{Name}' não foi encontrado.", name);
                    throw new KeyNotFoundException($"Admin com o nome '{name}' não encontrado.");
                }

                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter admin com o nome '{Name}'.", name);
                throw;
            }
        }

    }
}
