using Order.Entity.Entities;
using System.Threading.Tasks;

namespace Order.Entity.NewLayer.interfaces
{
    public interface IRoleRepository
    {

        IEnumerable<Role> GetAll();
        Role? GetById(int id);
        Role Add(string name);
        Role? Update(int id, string name);
        Role? Delete(int id);
    }
}
