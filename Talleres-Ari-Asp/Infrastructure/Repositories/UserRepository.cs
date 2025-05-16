using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public User GetByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email);
        public User GetByUserName(string username) => _context.Users.FirstOrDefault(u => u.UserName == username);
        public User GetById(int id) => _context.Users.Find(id);
        
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}