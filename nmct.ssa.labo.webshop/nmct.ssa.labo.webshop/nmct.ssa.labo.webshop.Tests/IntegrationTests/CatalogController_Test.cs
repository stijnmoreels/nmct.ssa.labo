using Microsoft.VisualStudio.TestTools.UnitTesting;
using nmct.ssa.labo.webshop.Controllers;
using nmct.ssa.labo.webshop.DataAccess.Repositories;
using nmct.ssa.labo.webshop.DataAccess.Services;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.Tests.Database;
using nmct.ssa.labo.webshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.Tests.IntegrationTests
{
    [TestClass]
    public class CatalogController_Test
    {
        private CatalogController catalogController = null;
        private DeviceController deviceController = null;
        private ICatalogService service = null;

        private IGenericRepository<OS> genericsOS = null;
        private IGenericRepository<FrameWork> genericsFramework = null;
        private IDeviceRepository deviceRepository = null;

        [TestInitialize]
        public void Setup()
        {
            //UnityConfig.RegisterComponents();

            new SetupDatabase().InitializeDatabase(new ApplicationDbContext(/*"CatalogConnection"*/));

            deviceRepository = new DeviceRepository();
            genericsOS = new GenericRepository<OS, ApplicationDbContext>();
            genericsFramework = new GenericRepository<FrameWork, ApplicationDbContext>();
            service = new CatalogService(deviceRepository, genericsOS, genericsFramework);
            
            catalogController = new CatalogController(service);
            deviceController = new DeviceController(service);
        }

        [TestMethod]
        public void Index_Test()
        {
            //AAA - principe
            
            //Act
            ViewResult view = catalogController.Index();

            //Assert
            Assert.IsNotNull(view);
            Assert.IsInstanceOfType(view.Model, typeof(List<Device>));
            Assert.IsTrue(((List<Device>)view.Model).Count == 5);
        }

        [TestMethod]
        public void Details_Test()
        {
            int id = 1;
            ViewResult view = catalogController.Details(id);
            Assert.IsNotNull(view);
            Assert.IsInstanceOfType(view.Model, typeof(Device));
            Assert.IsTrue(((Device)view.Model).Id == id);
        }

        [TestMethod]
        public void AddDevice_Test()
        {
            ViewResult view = deviceController.Create();
            Assert.IsNotNull(view);
            Assert.IsInstanceOfType(view.Model, typeof(CreateDeviceVM));
        }
    }
}
