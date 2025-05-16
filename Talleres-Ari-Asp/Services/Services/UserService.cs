using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Talleres_Ari_Asp.Services.Services
{
    public class UserService : IUserService {
    private readonly List<User> _users = new(); // Reemplaza con Entity Framework

    public User? GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

    public void CreateUser(User user) {
        user.Password = PasswordHasher.Hash(user.Password);
        _users.Add(user);
    }
}
}