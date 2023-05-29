using Order.Entity.Entities;

namespace Order.Entity.NewLayer.interfaces
{
    public interface IOrderItemRepository
    {

        IEnumerable<OrderItem> GetAll();
        OrderItem? GetById(int id);
        OrderItem Add(OrderItem orderItem);
        OrderItem? Update(OrderItem orderItem);
        OrderItem? Delete(int id);
    }
}
