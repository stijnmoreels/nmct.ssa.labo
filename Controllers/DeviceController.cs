using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.Controllers.ControllerInterface;
using nmct.ssa.labo.webshop.HandleLists;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.Controllers
{
    public class DeviceController : Controller, IControllerService
    {
        private static CreateDeviceVM _vm = new CreateDeviceVM();
        public ICatalogService CatalogService { get; set; }

        public DeviceController(ICatalogService service)
        {
            this.CatalogService = service;
        }

        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            HandleLists();
            return View(_vm);
        }

        public void HandleLists()
        {
            _vm.FrameWorks = new SelectList(CatalogService.GetFrameworks(), "Id", "Name");
            _vm.Os = new SelectList(CatalogService.GetOs(), "Id", "Name");

            //_vm.SelectedFrameWorks = new SelectList(new List<FrameWork>(), "Id", "Name");
            //_vm.SelectedOs = new SelectList(new List<OS>(), "Id", "Name");
        }

        public void HandleLists(CreateDeviceVM vm)
        {
            vm.FrameWorks = new SelectList(CatalogService.GetFrameworks(), "Id", "Name");
            vm.Os = new SelectList(CatalogService.GetOs(), "Id", "Name");

            vm.SelectedFrameWorks = new SelectList(new List<FrameWork>(), "Id", "Name");
            vm.SelectedOs = new SelectList(new List<OS>(), "Id", "Name");
        }

        [HttpPost]
        [Authorize]
        public RedirectToRouteResult Create(CreateDeviceVM vm)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Catalog");
            _vm.Device = vm.Device;
            if(vm.btnSend == null)
            {
                if(vm.btnAddOS != null)
                    return AddOS(vm);
                else if(vm.btnAddFrame != null)
                    return AddFrame(vm);
                return null;
            }
            else
            {
                // NEXT
                
                var lists = new HandleLists.HandleLists(CatalogService);
                _vm.Device.OS = lists.MakeListOS(null, vm.HiddenOS);
                _vm.Device.FrameWorks = lists.MakeListFrame(null, vm.HiddenFrame);
                //DeviceRepository.InsertDevice(_vm.Device);
                CatalogService.InsertDevice(_vm.Device);
                return RedirectToAction("Upload", "Catalog", new { id = _vm.Device.Id });
            }
        }

        #region Handle-OS-and-FrameWorks
        private RedirectToRouteResult AddFrame(CreateDeviceVM vm)
        {
            FrameWork frame = CatalogService.GetFrameWorkById(vm.SelectedFrameId);
            var lists = new HandleLists.HandleLists(CatalogService);

            _vm.Device.OS = lists.MakeListOS(null, vm.HiddenOS);
            _vm.Device.FrameWorks = lists.MakeListFrame(frame, vm.HiddenFrame);
            return RedirectToAction("Create");
        }

        private RedirectToRouteResult AddOS(CreateDeviceVM vm)
        {
            OS os = CatalogService.GetOsById(vm.SelectedOsId);
            var lists = new HandleLists.HandleLists(CatalogService);

            _vm.Device.OS = lists.MakeListOS(os, vm.HiddenOS);
            _vm.Device.FrameWorks = lists.MakeListFrame(null, vm.HiddenFrame);
            return RedirectToAction("Create");
        }
        #endregion

        [HttpGet]
        public RedirectToRouteResult SetFavorite(int id)
        {
            CatalogService.UpdateFavorite(id, true);
            return RedirectToAction("Favorites");
        }

        [HttpGet]
        public RedirectToRouteResult UnsetFavorite(int id)
        {
            CatalogService.UpdateFavorite(id, false);
            return RedirectToAction("Index", "Catalog");
        }

        [HttpGet]
        public ViewResult Favorites()
        {
            return View(CatalogService.GetFavoriteDevices());
        }
    }
}