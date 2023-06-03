using Order.Entity.Entities;

namespace Order.Entity.NewLayer.interfaces
{
    public interface IUserRepository
    {

        IEnumerable<User> GetAll();
        User? GetById(int id);
        User Add(string name);
        User? Update(int id, string name);
        User? Delete(int id);
    }
}
