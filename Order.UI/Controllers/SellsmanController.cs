using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.Repositories;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Order.Entity.NewLayer.Sellsman
{
    public class SellsmanController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;



        public SellsmanController(IProductRepository productRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7260/"); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }



        public async Task<IActionResult> Index()
        {

            try
            {
                var response = await _httpClient.GetAsync("/api/Sellsman/GetAll");

                if (response.IsSuccessStatusCode)
                {
                    var ProductsJson = await response.Content.ReadAsStringAsync();

                    var objMultipleModels = JsonConvert.DeserializeObject<Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>>(ProductsJson);
                    return View(objMultipleModels);

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
            }



            return View();

        }


       
        public async Task<IActionResult> GetItemUnitPrice(int itemId)
        {
            decimal unitPrice = 0;


            var response = await _httpClient.GetAsync($"/api/Sellsman/getItemUnitPrice?itemId={itemId}");




            string jsonResponse = await response.Content.ReadAsStringAsync();
            unitPrice = JsonConvert.DeserializeObject<decimal>(jsonResponse);




            return Json(unitPrice);
    }





        [HttpPost]
        public async Task<IActionResult> Index(SellsmanViewModel orderViewModel)
        {

            
                var response = await _httpClient.PostAsJsonAsync("/api/Sellsman/Create",orderViewModel);
            

            return Json(response);
        }


        
    }



}
