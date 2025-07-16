using Saas.Models;
using Saas.Services.DTOs.UserDto;

namespace Saas.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(UserCreateDto user);
        Task<bool> UpdateUserAsync(int id, UserCreateDto user);
        Task<bool> DeleteUserAsync(int id);
        Task<UserResponseDto?> GetUserByCpfAsync(string cpf);
        Task<User?> GetUserByEmailAsync(string email);
    }
}
