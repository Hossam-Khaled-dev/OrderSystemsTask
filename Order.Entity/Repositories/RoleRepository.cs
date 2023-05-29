using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;

namespace Order.Entity.Repositories
{
    public class RoleRepository : IRoleRepository
    {

        private readonly OrderDbContext _context;

        public RoleRepository(OrderDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Roles.ToList();
        }

        public Role? GetById(int id)
        {
            return _context.Roles.Find(id);
        }

        public Role Add(string name)
        {
            var role = new Role { Name = name };
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public Role? Update(int id, string name)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                role.Name = name;
                _context.SaveChanges();
            }
            return role;
        }

        public Role? Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
            return role;
        }

    }
}
