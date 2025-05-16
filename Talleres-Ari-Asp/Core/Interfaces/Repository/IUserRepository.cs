using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        User GetByEmail(string email);
        User GetByUserName(string username);
        User GetById(int id);
        void Add(User user);
        void Update(User user);
    }
}
