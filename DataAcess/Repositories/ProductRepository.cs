using Microsoft.EntityFrameworkCore;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;

namespace Order.Entity.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrderDbContext _context;

        public ProductRepository(OrderDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public Product Add(string name)
        {
            var product = new Product { Name = name };
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Product? Update(int id, string name)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                product.Name = name;
                _context.SaveChanges();
            }
            return product;
        }

        public Product? Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return product;
        }

    }
}
