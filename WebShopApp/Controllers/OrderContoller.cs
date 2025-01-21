using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using WebShopApp.Core.Contracts;
using WebShopApp.Infrastrucutre.Data.Domain;
using WebShopApp.Models.Order;

namespace WebShopApp.Controllers
{
    [Authorize]
    public class OrderContoller : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrderContoller(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }


        // GET: OrderContoller
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            List<OrderIndexVM> orders = _orderService.GetOrders()
            .Select(x => new OrderIndexVM
             {
                Id = x.Id,
                OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                UserId = x.UserId,
                User = x.User.UserName,
                ProductId = x.ProductId,
                Product = x.Product.ProductName,
                Picture = x.Product.Picture,
                Quantity = x.Quantity,
                Price = x.Price,
                Discount = x.Discount,
                TotalPrice = x.TotalPrice,
             }).ToList();
            return View(orders);
        }

        public ActionResult MyOrders()
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<OrderIndexVM> orders = _orderService.GetOrdersByUser(currentUserId)
                .Select(x => new OrderIndexVM
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate.ToString("dd-MMM-yyyy hh:mm", CultureInfo.InvariantCulture),
                    UserId = x.UserId,
                    User = x.User.UserName,
                    ProductId = x.ProductId,
                    Product = x.Product.ProductName,
                    Picture = x.Product.Picture,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Discount = x.Discount,
                    TotalPrice = x.TotalPrice,
                }).ToList();
            return View(orders);
        }

        // GET: OrderContoller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderContoller/Create
        public ActionResult Create(int id)
        {
            Product product = _productService.GetProductsById(id);
            if (product == null)
            {
                return NotFound();
            }

            OrderCreateVM order = new OrderCreateVM()
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                QuantityInStock = product.Quantity,
                Price = product.Price,
                Discount = product.Discount,
                Picture = product.Picture,
            };
            return View(order);
        }

        // POST: OrderContoller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateVM bindingModel)
        {
            string currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var product = this._productService.GetProductsById(bindingModel.ProductId);
            if (currentUserId == null || product == null || product.Quantity < bindingModel.Quantity || product.Quantity == 0)
            {
                return RedirectToAction("Denied", "Order");
            }
            if (ModelState.IsValid)
            {
                _orderService.Create(bindingModel.ProductId, currentUserId, bindingModel.Quantity);
            }
            return this.RedirectToAction("Index", "Product");
        }

        public ActionResult Denied()
        {
            return View();
        }

        // GET: OrderContoller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderContoller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderContoller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderContoller/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
