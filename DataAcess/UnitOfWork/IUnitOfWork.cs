using Order.Entity.Entities;
using Order.Entity.Repositories;

namespace Order.Entity.UnitOfWork
{
    public interface IUnitOfWork 
    {
        IRepository<Product> Products { get; }
        IRepository<User> Users { get; }
        IRepository<Entities.Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        IRepository<Entities.Role> Roles { get; }

        int Commit();


    }

}
