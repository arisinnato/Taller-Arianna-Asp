using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IUserService
    {
        User? GetByEmail(string email);
        void CreateUser(User user);
    }
}
