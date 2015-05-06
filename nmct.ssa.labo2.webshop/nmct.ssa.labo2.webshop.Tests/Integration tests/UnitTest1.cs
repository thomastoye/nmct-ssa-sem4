using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nmct.ssa.labo2.webshop.Tests.Database;
using nmct.ssa.labo2.webshop.Controllers;
using nmct.ssa.labo2.webshop.Models.DAL.Repositories;
using nmct.ssa.labo2.webshop.Services;
using nmct.ssa.labo2.webshop.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace nmct.ssa.labo2.webshop.Tests.Integration_tests
{
    [TestClass]
    public class IndexIntegrationTest
    {
        private DevicesController controller;
        private IProductService productService;
        private IGenericRepository<OS> repoOs;
        private IGenericRepository<ProgrammingFramework> repoFramework;
        private IDeviceRepository repoDevice;

        [TestInitialize]
        public void Setup()
        {
            new SetupDatabase().InitializeDatabase(new Models.ApplicationDbContext());

            repoDevice = new DeviceRepository();
            repoFramework = new GenericRepository<ProgrammingFramework>();
            repoOs = new GenericRepository<OS>();
            productService = new ProductService(repoOs, repoFramework, repoDevice);
            controller = new DevicesController(productService);
        }

        [TestMethod]
        public void TestMethod1()
        {
            ViewResult result = (ViewResult)controller.Index();
            IEnumerable<Device> devices = result.Model as IEnumerable<Device>;
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Device>));
            Assert.AreEqual(5, devices.Count());
        }
    }
}
