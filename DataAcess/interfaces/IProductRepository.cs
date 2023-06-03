using Order.Entity.Entities;

namespace Order.Entity.NewLayer.interfaces
{
    public interface IProductRepository
    {


        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        Product Add(string name);
        Product? Update(int id, string name);
        Product? Delete(int id);
    }
}
