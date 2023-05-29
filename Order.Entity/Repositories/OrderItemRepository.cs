using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;

namespace Order.Entity.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderDbContext _context;

        public OrderItemRepository(OrderDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> GetAll()
        {
            return _context.OrderItems.ToList();
        }

        public OrderItem? GetById(int id)
        {
            return _context.OrderItems.Find(id);
        }

        public OrderItem Add(OrderItem orderItem)
        {


            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return orderItem;
        }

        public OrderItem? Update(OrderItem orderItemNew)
        {
            var orderItem = _context.OrderItems.Find(orderItemNew.Id);
            if (orderItem != null)
            {
                
                orderItem.Quantity = orderItemNew.Quantity;
                orderItemNew.Price = orderItemNew.Price;
                orderItemNew.OrderId= orderItemNew.OrderId; 
                orderItemNew.ProductId=orderItemNew.ProductId;

                _context.SaveChanges();
            }
            return orderItem;
        }

        public OrderItem? Delete(int id)
        {
            var orderItem = _context.OrderItems.Find(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                _context.SaveChanges();
            }
            return orderItem;
        }
    }

}
