using Microsoft.AspNetCore.Mvc;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;

namespace Order.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderService;
        private readonly IUnitOfWork _unitOfWork;


        public OrderController(IOrderRepository orderService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _orderService = orderService;

        }

        public IActionResult Index()
        {
            var orders = _unitOfWork.Orders.GetAll();
            var orderViewModels = orders.Select(o => new SellsmanViewModel
            {
                Id = o.Id,
              
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalPrice
            }).ToList();

            return View(orderViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SellsmanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order.Entity.Entities.Order
                {
                    OrderDate = model.OrderDate,
                    TotalPrice= model.TotalAmount
                };

                _unitOfWork.Orders.Add(order);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new SellsmanViewModel
            {
                Id = order.Id,
                
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalPrice
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Edit(SellsmanViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = _unitOfWork.Orders.GetById(model.Id);

                if (order == null)
                {
                    return NotFound();
                }

                
                order.OrderDate = model.OrderDate;
                order.TotalPrice = model.TotalAmount;

                _unitOfWork.Orders.Update(order);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new SellsmanViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalPrice
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            _unitOfWork.Orders.Remove(order);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new SellsmanViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalPrice
            };

            return View(orderViewModel);
        }
    }

}
