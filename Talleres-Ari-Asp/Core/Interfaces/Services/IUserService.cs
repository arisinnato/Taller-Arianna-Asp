using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUserNameAsync(string userName);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
