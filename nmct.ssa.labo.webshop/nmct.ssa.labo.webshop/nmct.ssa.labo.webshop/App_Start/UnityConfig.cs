using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Data.Entity;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.Controllers;
using Microsoft.AspNet.Identity;
using System;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.businesslayer.Repositories;
using nmct.ssa.labo.webshop.businesslayer.Services;

namespace nmct.ssa.labo.webshop
{
    public static class UnityConfig 
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor()); //--> juist uit comment geplaatst
            container.RegisterType<ApplicationDbContext, ApplicationDbContext>(new InjectionConstructor());

            //container.RegisterType<IUserStore<ApplicationUser>, ApplicationUser>();
            //container.RegisterType<IGenericRepository<FrameWork>, GenericRepository<FrameWork>>();
            //container.RegisterType<IGenericRepository<OS>, GenericRepository<OS>>();

            container.RegisterType<IGenericRepository<FrameWork>, GenericRepository<FrameWork, ApplicationDbContext>>();
            container.RegisterType<IGenericRepository<OS>, GenericRepository<OS, ApplicationDbContext>>();
            container.RegisterType<IDeviceRepository, DeviceRepository>();
            container.RegisterType<IBasketRepository, BasketRepository>();
            container.RegisterType<IOrderResporitory, OrderRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<ICatalogService, CatalogService>();
            container.RegisterType<IBasketService, BasketService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterInstance(typeof(String), "CatalogConnection");

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}