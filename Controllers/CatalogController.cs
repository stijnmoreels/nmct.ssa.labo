using nmct.ssa.labo.webshop.Controllers.ControllerInterface;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.Constants;
using nmct.ssa.labo.webshop.Cookies;
using System.Text;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using System.Globalization;

namespace nmct.ssa.labo.webshop.Controllers
{
    // testetetetsetetsetsetsetsetsesetse 

    //dit komt van michiel
    public class CatalogController : Controller, IControllerService
    {
        public ICatalogService CatalogService { get; set; }
        public IBasketService BasketService { get; set; }
        public ICookieController CookieController { get; set; }

        public CatalogController(ICatalogService service, IBasketService basketService)
        {
            this.CatalogService = service;
            this.BasketService = basketService;
            this.CookieController = null;
        }

        [HttpGet]
        [OutputCache(Duration=10)]
        public ViewResult Index()
        {
            this.CookieController = this.CookieController ?? new CookieController(BasketService, User, Request, Response);
            BasketService.RefreshItemsInBasket(UserOrAnonymous());
            CatalogService.RefreshDevices();
            if (!User.Identity.IsAuthenticated && Request.Cookies[CookieAuth.COOKIE_AUTH] == null)
                CookieController.MakeCookie(DateTime.Now.ToString() + new Random().Next(0, 2000));
            //return View(CatalogService.GetDevices());
            return View(CatalogService.GetTranslatedDevices(CultureInfo.CurrentUICulture.Name));
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            DetailsDeviceVM vm = new DetailsDeviceVM();
            vm.Device = CatalogService.GetDeviceById(id);
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles=UserRoles.ADMIN_ROLE)]
        public ViewResult Upload(int id)
        {
            UploadVM vm = new UploadVM();
            vm.Device = CatalogService.GetDeviceById(id);
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles=UserRoles.ADMIN_ROLE)]
        public RedirectToRouteResult Upload(UploadVM vm)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            //string filename =  Path.GetFileName(file.FileName);
            //file.SaveAs(Server.MapPath(Path.Combine("~/DeviceImages", filename)));

            CatalogService.UpdatePicture(int.Parse(vm.Id), file);
            return RedirectToAction("Index");
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