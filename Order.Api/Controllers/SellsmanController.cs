using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;

namespace Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellsmanController : ControllerBase
    {

        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;


        public SellsmanController(IProductRepository productRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetAll")]

        public ActionResult GetAll()
        {
            var Users = _unitOfWork.Users.GetAll().Select(u => new SelectListItem
            {

                Text = u.UserName,
                Value = u.Id.ToString(),
                Selected = true
            });


            var Products = _unitOfWork.Products.GetAll().Select(p => new SelectListItem
            {

                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = true
            });
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>(
                    Users, Products);

            return Ok(objMultipleModels);
        }

        [HttpGet("getItemUnitPrice")]

        public IActionResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = _unitOfWork.Products.GetById(itemId).Price;
            return Ok(UnitPrice);
        }

        [HttpPost("Create")]
        public IActionResult Create(SellsmanViewModel objOrderViewModel)
        {

            var Orders = new Entity.Entities.Order
            {
                TotalPrice = objOrderViewModel.Total,


                OrderItems = objOrderViewModel.Items.Select(s => new Entity.Entities.OrderItem
                {
                    OrderId = s.OrderId,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    ProductId = s.ProductId,




                }).ToList(),

            };

            _unitOfWork.Orders.Add(Orders);
            string SuccessMessage = String.Empty;


            return Ok(SuccessMessage);
        }




    }
}
