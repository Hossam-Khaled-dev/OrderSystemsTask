

using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
public interface IRepository<T> where T : class
{


    IEnumerable<T> GetAll();
    T GetById(object id);
    void Add(T obj);
    void Update(T obj);
    void Remove(object id);
   
   

   

}