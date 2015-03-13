using Microsoft.VisualStudio.TestTools.UnitTesting;
using nmct.ssa.labo.webshop.businesslayer.Context;
using nmct.ssa.labo.webshop.businesslayer.Repositories;
using nmct.ssa.labo.webshop.businesslayer.Services;
using nmct.ssa.labo.webshop.Controllers;
using nmct.ssa.labo.webshop.models;
using nmct.ssa.labo.webshop.Models;
using nmct.ssa.labo.webshop.test.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace nmct.ssa.labo.webshop.test.IntegrationTests
{
    [TestClass]
    public class CatalogController_Test
    {
        private CatalogController controller = null;
        private ICatalogService service = null;
        private IGenericRepository<OS> repOS = null;
        private IGenericRepository<FrameWork> repFrame = null;
        private IDeviceRepository repDevice = null;

        [TestInitialize]
        public void Setup()
        {
            var context = new ApplicationDbContext("CatalogConnection");
            new SetupDatabase().InitializeDatabase(context);
            repDevice = new DeviceRepository(context);
            repFrame = new GenericRepository<FrameWork, ApplicationDbContext>();
            repOS = new GenericRepository<OS, ApplicationDbContext>();
            service = new CatalogService(repDevice, repOS, repFrame);
            controller = new CatalogController(service);
        }

        [TestMethod]
        public void Index_Test()
        {
            //Act 
            ViewResult view = (ViewResult)controller.Index();
            List<Device> devices = view.Model as List<Device>;

            //Assert 
            Assert.IsNotNull(view);
            Assert.IsNotInstanceOfType(view.Model, typeof(List<Device>));
            Assert.IsTrue(devices.Count == 6);
        }
    }
}
