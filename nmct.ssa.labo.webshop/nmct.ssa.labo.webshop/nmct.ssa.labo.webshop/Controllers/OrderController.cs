using nmct.ssa.labo.webshop.businesslayer.Services;
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
            Order order = new Order()
            {
                Zipcode = int.Parse(user.Zipcode),
                Address = user.Address,
                Firstname = user.Fristname,
                Lastname = user.Name,
                UserId = user.Id
            };
            return View(order);
        }

        [HttpPost]
        public RedirectToRouteResult Create(Order order)
        {
            List<BasketItem> items = BasketService.GetAllBasketItems(User.Identity.Name);
            List<OrderLine> orders = new List<OrderLine>();
            items.ForEach(i => orders.Add(new OrderLine() { Amount = i.Amount, Device = i.Device, TotalPrice = i.TotalPrice }));
            order.Orders = orders;

            OrderService.AddToQueue(order);
            BasketService.UnavailableBasket(items);
            return RedirectToAction("Finish");
        }

        [HttpGet]
        public ViewResult Finish()
        {
            return View();
        }
    }
}