using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {

            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: /Product
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Products.GetAll();

            var productViewModels = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Amount = p.Amount,
                
            });

            return Ok(productViewModels);
        }

        

        [HttpPost("Create")]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Amount = model.Amount

                };

                _unitOfWork.Products.Add(product);
                _unitOfWork.Commit();

            }

            return Ok(model);
        }

        

        [HttpPut("Edit")]
        public IActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _unitOfWork.Products.GetById(model.Id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Price = model.Price;

                _unitOfWork.Commit();

            }

            return Ok(model);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Remove(product.Id);
            _unitOfWork.Commit();
              return Ok();

        }
        
    }
}
