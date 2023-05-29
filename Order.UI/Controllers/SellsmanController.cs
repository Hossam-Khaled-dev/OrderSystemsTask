using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Order.Entity.Entities;
using Order.Entity.NewLayer.interfaces;
using Order.Entity.Repositories;
using Order.Entity.UnitOfWork;
using Order.Entity.ViewModels;

namespace Order.Entity.NewLayer.Sellsman
{
    public class SellsmanController : Controller
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



        public ActionResult Index()
        {
            var Users= _unitOfWork.Users.GetAll().Select(u=>new SelectListItem
            {

                Text = u.UserName,
                Value = u.Id.ToString(),
                Selected = true
            });


            var Products= _unitOfWork.Products.GetAll().Select(p => new SelectListItem
            {

                Text = p.Name,
                Value = p.Id.ToString(),
                Selected = true
            });
            var objMultipleModels = new Tuple<IEnumerable<SelectListItem>, IEnumerable<SelectListItem>>(
                    Users , Products);

            return View(objMultipleModels);
        }

        [HttpGet]

        public IActionResult getItemUnitPrice(int itemId)
        {
            decimal UnitPrice = _unitOfWork.Products.GetById(itemId).Price;
            return Json(UnitPrice);
        }

        [HttpPost]
        public IActionResult Index(SellsmanViewModel objOrderViewModel)
        {

            var Orders = new Entities.Order
            {
                TotalPrice=objOrderViewModel.Total,
                

                OrderItems = objOrderViewModel.Items.Select(s => new Entities.OrderItem
                {
                    OrderId = s.OrderId,
                    Price=s.Price,
                    Quantity=s.Quantity,
                    ProductId= s.ProductId,
                    
                    
                

                }).ToList(),
                
            };
          
             _unitOfWork.Orders.Add(Orders);
            string SuccessMessage = String.Empty;

           
            return Json(SuccessMessage);
        }









        //public IActionResult Index()
        //{
        //    // Get the list of products available for sale
        //    var products = _unitOfWork.Products.GetAll();

        //    // Pass the products to the view
        //    var productViewModels = products.Select(p => new SellsmanViewModel
        //    {
        //        Id = p.Id,
        //      TotalAmount=p.Amount,
        //        // Set other properties as needed
        //    });

        //    return View(productViewModels);
        //}

        //[HttpPost]
        //public IActionResult AddToOrder(Entities.Order neworder)
        //{
        //    // Retrieve the selected product
        //    var product = _unitOfWork.Products.GetById(neworder.Id);

        //    // Check if the product is available
        //    if (product != null && product.Amount > 0)
        //    {
        //        // Create a new order item and add it to the order
        //        var orderItem = new Entities.OrderItem
        //        {
        //            ProductId = product.Id,

        //            Price = product.Price,
        //            Quantity = 1
        //        };

        //        _unitOfWork.Orders.Add(neworder);

        //        // Decrease the product quantity by 1
        //        product.Amount--;

        //        _unitOfWork.Products.Update(product);
        //    }

        //    // Redirect back to the Sellsman page
        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public IActionResult RemoveFromOrder(int orderItemId)
        //{
        //    // Retrieve the order item
        //    var orderItem = _unitOfWork.OrderItems.GetById(orderItemId);

        //    // Check if the order item exists
        //    if (orderItem != null)
        //    {
        //        // Increase the product quantity by 1
        //        var product = _unitOfWork.Products.GetById(orderItem.ProductId);
        //        if (product != null)
        //        {
        //            product.Amount++;
        //            _unitOfWork.Products.Update(product);
        //        }

        //        // Remove the order item from the order
        //        _unitOfWork.OrderItems.Remove(orderItem);
        //    }

        //    // Redirect back to the Sellsman page
        //    return RedirectToAction("Index");
        //}

        //public IActionResult CalculateTotalPrice()
        //{
        //    // Get the list of order items
        //    var orderItems = _unitOfWork.OrderItems.GetAll();

        //    // Calculate the total price
        //    var totalPrice = orderItems.Sum(oi => oi.Price * oi.Quantity);

        //    // Pass the total price to the view
        //    return View(totalPrice);
        //}

        //public IActionResult PrintBill()
        //{
        //    var orderItems = _unitOfWork.OrderItems.GetAll();

        //    var totalPrice = orderItems.Sum(oi => oi.Price * oi.Quantity);

        //    ViewBag.OrderItems = orderItems;
        //    ViewBag.TotalPrice = totalPrice;

        //    return View();
        //}

        //public IActionResult ChooseProducts()
        //{
        //    // Logic to retrieve available products and display them for selection
        //    var products = _unitOfWork.Products.GetAll();
        //    var productViewModels = products.Select(p => new ProductViewModel
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Price = p.Price
        //    }).ToList();

        //    return View(productViewModels);
        //}

        //[HttpPost]
        //public IActionResult ChooseProducts(List<int> selectedProductIds)
        //{
        //    // Logic to handle the selected products and add them to the order
        //    // You can customize this logic based on your requirements

        //    if (selectedProductIds != null && selectedProductIds.Any())
        //    {
        //        foreach (var productId in selectedProductIds)
        //        {
        //            var product = _unitOfWork.Products.GetById(productId);

        //            if (product != null)
        //            {
        //                // Logic to add product to the current order

        //                _unitOfWork.Commit();

        //                // Update product quantity or any other relevant logic

        //                _unitOfWork.Commit();
        //            }
        //        }
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}
