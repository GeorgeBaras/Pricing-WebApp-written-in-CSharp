using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial.EF_DBContext;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.MyClasses;

namespace Tutorial.Controllers
{
    public class HomeController : Controller
    {
        static IUnityContainer myContainer = UnityContainerSingleton.getContainer();
        static VehicleRepository vehicleRepository = (VehicleRepository)myContainer.Resolve<IRepository>("vehicleRepository");
        static appDBContext db = (appDBContext)myContainer.Resolve<DbContext>("applicationDBContext");

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Valuation()
        {
            var vehicles = vehicleRepository.getAllEntries();

            ViewData["vehicles"] = vehicles;
            ViewBag.Title = "Pick your vehicle's id ";
            ViewBag.Message = "Pick your vehicle's id ";
            return View();
        }

        [HttpPost]
        public ActionResult Valuation(int vehicleId)
        {
            var vehicles = vehicleRepository.getAllEntries();
            if (vehicleId > vehicles.Count || vehicleId < 1) {
                ViewBag.Message = "Your input was invalid,try again ";
                return View();
            }


            ViewBag.Title = "PriceBands for "+ vehicles[vehicleId].make +" "+ vehicles[vehicleId].model;

            return View("VehiclesView",vehicles[vehicleId]);
        }
        
    }
}
