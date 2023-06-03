using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Order.Entity.Entities;
using Order.Entity.UnitOfWork;
using System.Linq.Expressions;

namespace Order.Entity.Repositories
{
    

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OrderDbContext _context;
        private readonly DbSet<T> _dbSet;
        

        public Repository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();

        }


        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T obj)
        {
            _dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Remove(object id)
        {
            T entity = _dbSet.Find(id);
            Remove(entity);
        }

        public void Remove(T obj)
        {
            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _dbSet.Attach(obj);
            }
            _dbSet.Remove(obj);
        }




    }

}
