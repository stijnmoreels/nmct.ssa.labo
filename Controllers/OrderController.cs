using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.Constants;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        public IOrderService OrderService { get; set; }
        public IBasketService BasketService { get; set; }
        public IUserService UserService { get; set; }

        public OrderController(IOrderService service, IBasketService basketService, IUserService userService)
        {
            this.OrderService = service;
            this.BasketService = basketService;
            this.UserService = userService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ApplicationUser user = UserService.GetAllUserValues(User.Identity.Name);
            List<BasketItem> items = BasketService.GetAllBasketItems(User.Identity.Name);
            List<OrderLine> orders = new List<OrderLine>();
            items.ForEach(i => orders.Add(new OrderLine() { Amount = i.Amount, Device = i.Device, TotalPrice = i.TotalPrice }));
            OrderVM vm = new OrderVM();
            int temp;
            vm.Order = new Order()
            {
                Zipcode = Int32.TryParse(user.Zipcode, out temp) ? temp : 0,
                Address = user.Address ?? "",
                Firstname = user.Fristname ?? "",
                Lastname = user.Name ?? "",
                UserId = user.Id,
                Deleted = false,
                Orders = orders,
                TotalPrice = TotalPriceCalculator.CalculateTotalPrice(items)
            };
            vm.Couriers = OrderService.GetCouriers();
            return View(vm);
        }

        [HttpPost]
        public RedirectToRouteResult Create(OrderVM vm)
        {
            if (vm.CourierId == 0)
                return RedirectToAction("Create");
            //OrderService.SendMail(order);
            List<BasketItem> items = BasketService.GetAllBasketItems(User.Identity.Name);
            List<OrderLine> orders = new List<OrderLine>();
            items.ForEach(i => orders.Add(new OrderLine() { Amount = i.Amount, Device = i.Device, TotalPrice = i.TotalPrice }));
            BasketService.UnavailableBasket(items);

            vm.Order.CourierId = vm.CourierId;
            vm.Order.Orders = orders;
            vm.Order.TotalPrice = orders.Sum(o => o.TotalPrice)
                + OrderService.GetCourier(vm.CourierId).Price;
            OrderService.AddToQueue(vm.Order);
            OrderService.SendMail(vm.Order);
            return RedirectToAction("Finish");
        }

        [HttpGet]
        public ViewResult Finish()
        {
            return View();
        }

        [HttpGet]
        public ViewResult PlacedOrders()
        {
            BasketService.RefreshItemsInBasket(UserOrAnonymous());
            ApplicationUser user = UserService.GetAllUserValues(User.Identity.Name);
            List<Order> orders = OrderService.GetOrdersFromUser(user.Id);
            foreach (Order order in orders)
            {
                List<OrderLine> items = OrderService.GetOrderLinesFromOrder(order.Id);
                order.Orders = items;
                //order.TotalPrice = TotalPriceCalculator.CalculateTotalPrice(items);
            }
            return View(orders);
        }

        public string UserOrAnonymous()
        {
            bool auth = User.Identity.IsAuthenticated, 
                cookie = Request.Cookies[CookieAuth.COOKIE_AUTH] != null;
            return auth ? User.Identity.Name : cookie ? 
                Request.Cookies[CookieAuth.COOKIE_AUTH].Value : null;
        }
    }
}