using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using Saas.Models;
using Saas.Repository.UserRep;
using Saas.Services.DTOs.UserDto;

namespace Saas.Services.UserServices
{
    public class UserService : IUserService{

        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        

        
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper, IPasswordHasher<User> passwordHasher){
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> AddUserAsync(UserCreateDto userDto)
        {
            if (userDto == null)
            {
                _logger.LogError("Erro: Tentativa de inserir usuário nulo");
                throw new ArgumentNullException(nameof(userDto), "Erro: O Usuário não pode ser nulo");

            }
            try
            {
                var cpf = await _userRepository.GetUserByCpfAsync(userDto.UserCpf);
                if (cpf != null) {
                    _logger.LogWarning($"Esse cpf {userDto.UserCpf} Já foi cadastrado.");
                    return false;
                }

                var email = await _userRepository.GetUserByEmailAsync(userDto.UserEmail);
                if (email != null) {
                    _logger.LogWarning($"Esse email {userDto.UserEmail} Já foi cadastrado.");
                    throw new Exception($"Esse email {userDto.UserEmail} Já foi cadastrado.");
                }

                var user = _mapper.Map<User>(userDto);
                user.PasswordHash = _passwordHasher.HashPassword(user, userDto.PasswordHash);
                await _userRepository.AddUserAsync(user);
                _logger.LogInformation($"Usuário {userDto} adicionado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Erro inesperado: Não foi possivel adicionar usuário {userDto}.");
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Erro: Usuário com ID {Id} não encontrado para exclusão.", id);
                throw new InvalidOperationException($"Usuário com ID {id} não encontrado.");
            }
            try
            {
                await _userRepository.DeleteUserAsync(id);
                _logger.LogInformation($"Usuário deletado: {id}");
                return true;
            }
            catch (Exception ex){
                _logger.LogError(ex,"Erro Inesperado: Não foi possivel deletar o usuário.");
                throw;
            }
        }
        public async Task<UserResponseDto?> GetUserByCpfAsync(string cpf)
        {
            if (!string.IsNullOrEmpty(cpf)){
                _logger.LogWarning("Cpf vazio ou nulo");
                throw new InvalidOperationException("Cpf nulo ou vazio");
            }
            try
            {
                var user = await _userRepository.GetUserByCpfAsync(cpf);
                var result = _mapper.Map<UserResponseDto>(user);
                _logger.LogInformation($"Usuário retornado pelo cpf");
                return result;
            }
            catch (Exception ex){
                _logger.LogError(ex,"Erro inesperado ao retornar usuário pelo cpf");
                throw;
            }
          
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _logger.LogWarning("Email nulo ou vazio ");
                throw new InvalidOperationException("Email nulo ou vazio");
            }

            try
            {
                var user = await _userRepository.GetUserByEmailAsync(email);
                _logger.LogInformation($"Usuário retornado pelo Email {email}");
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao retornar usuário pelo Email {email}");
                throw;
            }
        }
        public Task<bool> UpdateUserAsync(int id, UserCreateDto user)
        {
            throw new NotImplementedException();
        }
    }
}
