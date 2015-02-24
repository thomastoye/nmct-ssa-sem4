using nmct.ssa.labo2.webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.labo2.webshop.Controllers
{
    public class DeviceController : Controller
    {
        public ActionResult Index()
        {
            return View(new DeviceRepository().GetDevices());
        }

        public ActionResult Details(int id)
        {
            return View(new DeviceRepository().GetDevice(id));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Device dev)
        {
            var returnedId = new DeviceRepository().InsertDevice(dev);
            return RedirectToAction("Details", new { id = returnedId} );
        }
    }
}