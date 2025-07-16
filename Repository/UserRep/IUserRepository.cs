using Saas.Models;

namespace Saas.Repository.UserRep
{
    public interface IUserRepository{
        Task AddUserAsync( User user);
        Task<bool>UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByNameAsync(string name);
        Task<User?> GetUserByCpfAsync(string cpf);
        Task<User?> GetUserByPhoneAsync(string phone);
        Task<User?> GetUserByEmailAsync(string email);

    }
}
