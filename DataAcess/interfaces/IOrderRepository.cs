namespace Order.Entity.NewLayer.interfaces;

public interface IOrderRepository
{
    IEnumerable<Entities.Order> GetAll();
    Entities.Order? GetById(int id);
    Entities.Order Add(Entities.Order order);
    Entities.Order? Update(Entities.Order order);
    Entities.Order? Delete(int id);
}

