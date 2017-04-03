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
        static PriceRecordRepository priceRecordRepository = (PriceRecordRepository)myContainer.Resolve<IRepository>("priceRecordRepository");
        static appDBContext db = (appDBContext)myContainer.Resolve<DbContext>("applicationDBContext");

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Valuation()
        {
            var vehicles = vehicleRepository.getAllEntries();
            if (vehicles.Count != 0)
            {
                ViewData["vehicles"] = vehicles;
            }
            else {
                ViewData["vehicles"] = new List<Vehicle>();
            }
            ViewBag.Title = "Pick your vehicle's id ";
            ViewBag.Message = "Pick your vehicle's id ";
            return View();
        }

        [HttpPost]
        public ActionResult Valuation(int vehicleId)
        {
            int vehicleSerial = vehicleId - 1;
            var vehicles = vehicleRepository.getAllEntries();
            ViewBag.Title = "PriceBands for " + vehicles[vehicleSerial].make + " " + vehicles[vehicleSerial].model;
            PriceRecord priceRecord = priceRecordRepository.getPriceRecordByLookupCode(vehicles[vehicleSerial].lookupCode);
            if (priceRecord != null)
            {
                return View("VehicleDetailsView", priceRecord);
            }
            return View();

        }

        public ActionResult ValueRecalculation(string lookupCode) {
            Vehicle vehicle = vehicleRepository.getVehicleByLookupCode(lookupCode);
            CAPValuationCalculator calculator = new CAPValuationCalculator();
            vehicle.value = calculator.calculate(priceRecordRepository.getPriceRecordByLookupCode(lookupCode), vehicle.mileage);
            return View("ValueRecalculation", vehicle);
        }



    }
}
