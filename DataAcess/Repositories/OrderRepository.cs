using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.Repositories;

namespace Order.Entity.Entities;

    public class OrderRepository :  IOrderRepository
    {
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Entities.Order> GetAll()
    {
        return _context.Orders.ToList();
    }

    public Entities.Order? GetById(int id)
    {
        return _context.Orders.Find(id);
    }

    public Entities.Order Add(Order neworder)
    {
        
        _context.Orders.Add(neworder);
        _context.SaveChanges();
        return neworder;
    }

    public Entities.Order? Update(Order neworder)
    {
        var order = _context.Orders.Find(neworder.Id);
        if (order != null)
        {
            order.TotalPrice= neworder.TotalPrice;
            order.OrderDate = neworder.OrderDate;





            _context.SaveChanges();
        }
        return order;
    }

    public Entities.Order? Delete(int id)
    {
        var order = _context.Orders.Find(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }
        return order;
    }
}


