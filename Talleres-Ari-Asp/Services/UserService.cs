using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Talleres_Ari_Asp.Services
{
    public class UserService {
    private readonly List<User> _users = new(); 

    public User? GetByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

    public void CreateUser(User user) {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Hash de contraseña
        _users.Add(user);
    }
}
}