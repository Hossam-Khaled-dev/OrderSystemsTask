using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;
using Order.Entity.Repositories;

namespace Order.Entity.UnitOfWork

{
    public class UnitOfWork : IUnitOfWork
    {

        private bool _disposed;
        private readonly OrderDbContext _context;
        private IRepository<Product> _productRepository;
        private IRepository<User> _userRepository;
        private IRepository<Entities.Order> _orderRepository;
        private IRepository<OrderItem> _orderItemRepository;
        private IRepository<Entities.Role> _roleRepository;


        public UnitOfWork(OrderDbContext context)
        {
            _context = context;
        }
        public IRepository<Product> Products => _productRepository ??= new Repository<Product>(_context);
        public IRepository<User> Users => _userRepository ??= new Repository<User>(_context);
        public IRepository<Entities.Order> Orders => _orderRepository ??= new Repository<Entities.Order>(_context);
        public IRepository<OrderItem> OrderItems => _orderItemRepository ??= new Repository<OrderItem>(_context);
        public IRepository<Entities.Role> Roles => _roleRepository ??= new Repository<Entities.Role>(_context);


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        int IUnitOfWork.Commit()
        {
            return _context.SaveChanges();
        }
    }


}
