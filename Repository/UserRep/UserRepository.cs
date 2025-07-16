using System;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Saas.Infrastructure.Context;
using Saas.Models;

namespace Saas.Repository.UserRep
{
    public class UserRepository : IUserRepository{
        
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(AppDbContext appDbContext, ILogger<UserRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }


        public async Task AddUserAsync(User user){
            try
            {
                await _appDbContext.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Usuário {user.UserName} adicionado com sucesso.");
            } 
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Erro ao adicionar usuario {user.UserName} no banco de dados.");
                throw new Exception("Erro ao salvar usuário no banco de dados.", ex);
            }
            catch (Exception ex){
                _logger.LogError(ex,"Ocorreu um erro inesperado ao adicionar o usuário ao banco de dados;");
                throw new Exception("Ocorreu um erro inesperado ao adicionar o usuário ao banco de dados;",ex);
            }
                
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _appDbContext.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"Usuário com ID {id} não encontrado.");
                    return false;
                }
                _appDbContext.Remove(user);
                await _appDbContext.SaveChangesAsync();
                _logger.LogInformation($"Usuario com ID {id} foi removido com sucesso");
                return true;

            }
            catch(DbUpdateException ex){
                _logger.LogError(ex,$"Erro ao remover usuario com ID {id} no banco de dados.");
                throw new Exception($"Erro ao remover o usuário com ID {id} no banco de dados.", ex);

            }
            catch (Exception ex) {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao remover o usuário com ID {id} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao remover o usuário com ID {id} ao banco de dados;", ex);
            }
        }
        public async Task<User?> GetUserByCpfAsync(string cpf){
            try
            {
                var user = await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserCpf == cpf);
                if (user == null)
                {
                    _logger.LogWarning($"Usuário com CPF {cpf} não encontrado.");
                }
                return user;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário com CPF {cpf}: {ex.Message}");
                throw new Exception($"Erro ao buscar usuário com CPF {cpf}.", ex);
            }
            catch (Exception ex){
                _logger.LogError(ex, $"Erro inesperado ao buscar usuário com CPF {cpf}.");
                throw new Exception($"Erro inesperado ao buscar usuário com CPF {cpf}.", ex);
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserEmail == email);
                if (user == null)
                {
                    _logger.LogWarning($"Usuário com Email {email} não encontrado.");
                }
                return user;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário com Email {email}: {ex.Message}");
                throw new Exception($"Erro ao buscar usuário com Email {email}.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao buscar usuário com Email {email}.");
                throw new Exception($"Erro inesperado ao buscar usuário com Email {email}.", ex);
            }
        }
        
        public async Task<User?> GetUserByIdAsync(int id){

            try
            {
                var user = await _appDbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
                if (user == null)
                {
                    _logger.LogWarning($"Usuário com ID {id} não encontrado.");
                }
                return user;

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário com ID {id} no banco de dados.");
                throw new Exception($"Erro ao buscar usuário com ID {id} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Ocorreu um erro inesperado ao buscar o usuário com ID {id} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao buscar o usuário com ID {id} ao banco de dados;", ex);
            }
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == name);
                if (user == null)
                {
                    _logger.LogWarning($"Usuário com Nome {name} não encontrado.");
                }
                return user;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Erro ao buscar usuário com Nome {name} no banco de dados.");
                throw new Exception($"Erro ao buscar usuário com Nome {name} no banco de dados.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao buscar o usuário com Nome {name} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao buscar o usuário com Nome {name} ao banco de dados;", ex);
            }
        }

        public async Task<User?> GetUserByPhoneAsync(string phone)
        {
            try{
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserPhone == phone);
                if (user == null){
                    _logger.LogWarning($"Usuário com Número {phone} não encontrado.");
                }
                return user;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex,$"Erro ao buscar usuário com Número {phone} no banco de dados.");
                throw new Exception($"Erro ao buscar usuário com Número {phone} no banco de dados.",ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao buscar o usuário com Nome {phone} ao banco de dados;");
                throw new Exception($"Ocorreu um erro inesperado ao buscar o usuário com Nome {phone} ao banco de dados;", ex);
            }
        }

        public async Task<bool> UpdateUserAsync(int id, User user){
           throw new NotImplementedException();
        }
     

    }
}
