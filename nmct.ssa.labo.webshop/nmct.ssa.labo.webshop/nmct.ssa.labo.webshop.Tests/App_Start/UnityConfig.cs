using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using nmct.ssa.labo.webshop.DataAccess.Repositories;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.DataAccess.Services;

namespace nmct.ssa.labo.webshop.Tests
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ICatalogService, CatalogService>();
            container.RegisterType<IGenericRepository<FrameWork>, GenericRepository<FrameWork, ApplicationDbContext>>();
            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS, ApplicationDbContext>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
           
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}