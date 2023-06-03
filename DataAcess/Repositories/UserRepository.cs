using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;

namespace Order.Entity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OrderDbContext _context;

        public UserRepository(OrderDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Add(string name)
        {
            var user = new User { UserName= name };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User? Update(int id, string name)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.UserName = name;
                _context.SaveChanges();
            }
            return user;
        }

        public User? Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return user;
        }
    }
}
