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

namespace nmct.ssa.labo.webshop.Controllers
{
    // testetetetsetetsetsetsetsetsesetse 

    //dit komt van michiel
   [Authorize]
    public class CatalogController : Controller, IControllerService
    {
        public ICatalogService Service { get; set; }

        public CatalogController(ICatalogService service)
        {
            this.Service = service; 
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(Service.GetDevices());
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            DetailsDeviceVM vm = new DetailsDeviceVM();
            vm.Device = Service.GetDeviceById(id);
            return View(vm);
        }

        [HttpGet]
        public ViewResult Upload(int id)
        {
            UploadVM vm = new UploadVM();
            vm.Device = Service.GetDeviceById(id);
            return View(vm);
        }

        [HttpPost]
        public RedirectToRouteResult Upload(UploadVM vm)
        {
            HttpPostedFileBase file = Request.Files["Image"];
            //string filename =  Path.GetFileName(file.FileName);
            //file.SaveAs(Server.MapPath(Path.Combine("~/DeviceImages", filename)));

            Service.UpdatePicture(int.Parse(vm.Id), file);
            return RedirectToAction("Index");
        }
    }
}