using Microsoft.AspNetCore.Mvc;
using Order.Entity.Repositories;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace Order.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;



        public ProductController(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7260/"); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: /Product
        public async Task<IActionResult> Index()
        {

            try
            {
                var response = await _httpClient.GetAsync("/api/Product/GetAll"); 

                if (response.IsSuccessStatusCode)
                {
                    var ProductsJson = await response.Content.ReadAsStringAsync();
                    var ProductViewModels = JsonConvert.DeserializeObject<List<ProductViewModel>>(ProductsJson);

                    return View(ProductViewModels);
                }

                ViewBag.ErrorMessage = $"Error: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return View();
        }

      
        

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var response = await _httpClient.PostAsJsonAsync("/api/Product/Create", model); 

                    if (response.IsSuccessStatusCode)
                    {
                        
                        return RedirectToAction("Index");
                    }

                    ViewBag.ErrorMessage = $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return View("Create", model); 
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
               
            };

            return View(model);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    

                    var response = await _httpClient.PutAsJsonAsync($"/api/Product/Edit?id{id}", model); 

                    if (response.IsSuccessStatusCode)
                    {
                        // Product update successful
                        return RedirectToAction("Index");
                    }

                    // Handle unsuccessful response
                    ViewBag.ErrorMessage = $"Error: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return View("Edit", model); 
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Product/Delete?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.ErrorMessage = $"Error: {response.StatusCode}";
            }
            catch (Exception ex)
            {
                // Handle exception
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }

            return RedirectToAction("Index"); 
        }
    }
}
