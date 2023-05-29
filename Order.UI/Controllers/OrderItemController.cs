using Microsoft.AspNetCore.Mvc;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;
using Order.Entity.Entities;

using System.Net.NetworkInformation;

namespace Order.Entity.NewLayer.OrderItem
{
    public class OrderItemController : Controller
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderItemRepository _orderItemService;
        public OrderItemController(IUnitOfWork unitOfWork, IOrderItemRepository orderItemService)
        {
            _unitOfWork = unitOfWork;
            _orderItemService = orderItemService;
        }
        public IActionResult Index()
        {
            var orderItems = _unitOfWork.OrderItems.GetAll();
            var orderItemViewModels = orderItems.Select(oi => new OrderItemViewModel
            {
                Id = oi.Id,
                Quantity = oi.Quantity,
                Price = oi.Price,
               Total = oi.Quantity * oi.Price
            }).ToList();

            return View(orderItemViewModels);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderItemViewModel model)
        {
            if (ModelState.IsValid)
            {

                var orderItem = new Order.Entity.Entities.OrderItem
                {
                    Quantity = model.Quantity,
                    Price = model.Price
                };

                _unitOfWork.OrderItems.Add(orderItem);
                _unitOfWork.Commit(); // Save changes

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var orderItem = _unitOfWork.OrderItems.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            var orderItemViewModel = new OrderItemViewModel
            {
                Id = orderItem.Id,
               // Name = orderItem.Name,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };

            return View(orderItemViewModel);
        }

        [HttpPost]
        public IActionResult Edit(OrderItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var orderItem = _unitOfWork.OrderItems.GetById(model.Id);

                if (orderItem == null)
                {
                    return NotFound();
                }

                //orderItem.Name = model.Name;
               // orderItem.Quantity = model.Quantity;
                //orderItem.Price = model.Price;

               _unitOfWork.OrderItems.Update(orderItem);
                _unitOfWork.Commit(); // Save changes

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var orderItem = _unitOfWork.OrderItems.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            var orderItemViewModel = new OrderItemViewModel
            {
                Id = orderItem.Id,
               // Name = orderItem.Name,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };

            return View(orderItemViewModel);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var orderItem = _unitOfWork.OrderItems.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            _unitOfWork.OrderItems.Remove(orderItem);
            _unitOfWork.Commit(); // Save changes

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var orderItem = _unitOfWork.OrderItems.GetById(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            var orderItemViewModel = new OrderItemViewModel
            {
                Id = orderItem.Id,
              //  Name = orderItem.Name,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };

            return View(orderItemViewModel);
        }
    }
}
