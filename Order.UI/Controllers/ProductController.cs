using Microsoft.AspNetCore.Mvc;
using Order.Entity.Repositories;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;

namespace Order.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IProductRepository productRepository , IUnitOfWork unitOfWork)
        {
            
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: /Product
        public IActionResult Index()
        {
            var products = _unitOfWork.Products.GetAll();
            var productViewModels = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                // Set other properties as needed
            });

            return View(productViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Amount=model.Amount
                    
                    // Set other properties as needed
                };

                _unitOfWork.Products.Add(product);
                _unitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var product = _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                // Set other properties as needed
            };

            return View(model);
        }

        [HttpPost]
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
                // Update other properties as needed

                _unitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var product = _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Remove(product.Id);
            _unitOfWork.Commit();

            return RedirectToAction("Index");
        }
    }
}
