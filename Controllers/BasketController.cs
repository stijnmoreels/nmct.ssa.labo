using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.Constants;
using nmct.ssa.labo.webshop.Controllers.ControllerInterface;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using nmct.ssa.labo.webshop.Cookies;
using nmct.ssa.labo.webshop.businesslayer.Caching;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;

namespace nmct.ssa.labo.webshop.Controllers
{
    public class BasketController : Controller, IControllerService
    {
        public ICatalogService CatalogService { get; set; }
        public IBasketService BasketService { get; set; }
        public ICookieController CookieController { get; set; }


        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            this.BasketService = basketService;
            this.CatalogService = catalogService;
            this.CookieController = null;
        }

        [HttpGet]
        public ActionResult Index()
        {
            this.CookieController = this.CookieController ?? 
                new CookieController(BasketService, User, Request, Response);
            BasketVM vm = new BasketVM();
            List<BasketItem> items = CookieController.GetItems();
            vm.Baskets = items;
            vm.TotalPrice = TotalPriceCalculator.CalculateTotalPrice(items);
            return View(vm);
        }

        [HttpPost]
        public RedirectToRouteResult Index(BasketVM vmd)
        {
            string user = UserOrAnonymous();
            if (user != null)
                BasketService.UpdateBasket(vmd.Baskets, user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult AddToBasket(DetailsDeviceVM vm)
        {
            string user = UserOrAnonymous();
            if (user != null)
                BasketService.AddToBasket(CatalogService.GetDeviceById(vm.DeviceId), vm.Amount, user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult DeleteFromBasket(int id)
        {
            BasketService.AvailableUnavailableBasket(id, false);
            return RedirectToAction("Index");
        }

        public int ItemsBasket()
        {
            string user = UserOrAnonymous();
            return user != null ? 
                BasketService.GetItemsInBasket(user) : 0;
        }

        public string UserOrAnonymous()
        {
            bool auth = User.Identity.IsAuthenticated, cookie = 
                Request.Cookies[CookieAuth.COOKIE_AUTH] != null;
            return auth ? User.Identity.Name : cookie ? 
                Request.Cookies[CookieAuth.COOKIE_AUTH].Value : null;
        }
    }
}