using nmct.ssa.labo.webshop.businesslayer.Services.Interfaces;
using nmct.ssa.labo.webshop.Constants;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.Controllers
{
    public class CultureController : Controller
    {
        private ICultureService cultureService = null;

        public CultureController(ICultureService cultureService)
        {
            this.cultureService = cultureService;
        }

        [HttpGet]
        public PartialViewResult ChangeCulture()
        {
            List<Culture> cultures = cultureService.GetAllAvailableCultures();
            string cookie = CheckCultureCookie();
            string filter = !cookie.Equals(string.Empty) ? cookie : CultureInfo.CurrentUICulture.Name;
            Culture selCulture = cultures.Where(c => c.Name.Equals(filter)).SingleOrDefault<Culture>();
            CultureVM vm = new CultureVM();
            vm.Cultures = new SelectList(cultures, "Id", "Name", selCulture ?? cultures[0]);
            vm.SelectedCulture = selCulture.Id;
            return PartialView(vm);
        }

        private string CheckCultureCookie()
        {
            string filter = string.Empty;
            if (Request.Cookies["Culture"] != null)
                filter = Request.Cookies["Culture"].Value;
            return filter;
        }

        [HttpPost]
        public RedirectToRouteResult ChangeCulture(CultureVM vm)
        {
            string culture = cultureService.GetCultureFromId(vm.SelectedCulture).Name;
            HttpCookie cookie = new HttpCookie(CookieAuth.COOKIE_AUTH, culture);
            cookie.Expires = DateTime.Now.AddDays(5);
            Response.SetCookie(cookie);
            return RedirectToAction("Index", "Catalog");
        }
    }
}