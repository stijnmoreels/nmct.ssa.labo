using nmct.ssa.labo.webshop.businesslayer.Calculators;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.Controllers.ControllerInterface;
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
    public class BasketController : Controller, IControllerService
    {
        public ICatalogService Service { get; set; }
        public IBasketService BasketService { get; set; }

        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            this.BasketService = basketService;
            this.Service = catalogService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<BasketItem> items = BasketService.GetAllBasketItems(User.Identity.Name);

            BasketVM vm = new BasketVM();
                vm.TotalPrice = TotalPriceCalculator.CalculateTotalPrice(items);
                vm.Baskets = items;
            
            return View(vm);
        }

        [HttpPost]
        public RedirectToRouteResult Index(BasketVM vmd)
        {
            BasketService.UpdateBasket(vmd.Baskets, User.Identity.Name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToRouteResult AddToBasket(DetailsDeviceVM vm)
        {
            //string id = Membership.GetUser(User.Identity.Name).ProviderUserKey.ToString();
            //RENT

            BasketService.AddToBasket(Service.GetDeviceById(vm.DeviceId), vm.Amount, User.Identity.Name);
            return RedirectToAction("Index");
        }

        public int ItemsBasket()
        {
            return BasketService.GetAllBasketItems(User.Identity.Name).Count;
        }
    }
}