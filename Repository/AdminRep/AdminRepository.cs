using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.AdminRep
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(AppDbContext context, ILogger<AdminRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAdminAsync(Admin admin)
        {
            try
            {
                await _context.Admins.AddAsync(admin);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Admin com ID {admin.AdminId} adicionado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar Admin.");
                throw;
            }
        }

        public async Task DeleteAdminByIdAsync(int id)
        {
            try
            {
                var existingAdmin = await _context.Admins.FindAsync(id);
                if (existingAdmin == null)
                {
                    _logger.LogWarning($"Admin com ID {id} não encontrado.");
                    return;
                }

                _context.Admins.Remove(existingAdmin);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Admin com ID {id} removido com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao remover Admin com ID {id}.");
                throw;
            }
        }
        public async Task<Admin?> GetAdminByNameAsync(string name)
        {
            try
            {
                var admin = await _context.Admins
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Name == name);

                if (admin == null)
                {
                    _logger.LogWarning("Admin com nome '{Name}' não encontrado.", name);
                }

                return admin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar Admin por nome '{Name}'.", name);
                throw;
            }
        }
    }
}
