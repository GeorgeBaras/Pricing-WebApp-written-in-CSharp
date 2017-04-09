using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial.EF_DBContext;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.Models;
using Tutorial.My_EntityClasses;
using Tutorial.MyClasses;

namespace Tutorial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<String> lookupCodes = new List<String>();
            foreach (Vehicle vehicle in MasterRepository.vehicleRepository.getAllEntries())
            {
                lookupCodes.Add(vehicle.lookupCode);
            }
            LookUpCodesViewModel viewModel = new LookUpCodesViewModel(lookupCodes);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(String lookUpCode)
        {
            Vehicle vehicle = RecalculateVehicleValue(lookUpCode); if (vehicle == null)
            {
                return View("Error");
            }
            return View("ValueRecalculation", vehicle);
        }

        public ActionResult Valuation()
        {
            var vehicles = MasterRepository.vehicleRepository.getAllEntries();
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
            var vehicles = MasterRepository.vehicleRepository.getAllEntries();
            ViewBag.Title = "PriceBands for " + vehicles[vehicleSerial].make + " " + vehicles[vehicleSerial].model;
            PriceRecord priceRecord = MasterRepository.priceRecordRepository.getPriceRecordByLookupCode(vehicles[vehicleSerial].lookupCode);
            if (priceRecord != null)
            {
                return View("VehicleDetailsView", priceRecord);
            }
            return View();

        }

        public ActionResult ValueRecalculation(string lookupCode)
        {
            Vehicle vehicle = RecalculateVehicleValue(lookupCode); if (vehicle == null)
            {
                return View("Error");
            }
            return View("ValueRecalculation", vehicle);
        }

        private static Vehicle RecalculateVehicleValue(string lookupCode)
        {
            Vehicle vehicle = MasterRepository.vehicleRepository.getVehicleByLookupCode(lookupCode);
            CAPValuationCalculator calculator = new CAPValuationCalculator();
            if (vehicle != null)
            {
                vehicle.value = calculator.calculate(MasterRepository.priceRecordRepository.getPriceRecordByLookupCode(lookupCode), vehicle.mileage);
            }
            return vehicle;
        }

        public ActionResult HomeSearch() {
            List<String> lookupCodes = new List<String>();
            foreach (Vehicle vehicle in MasterRepository.vehicleRepository.getAllEntries()) {
                lookupCodes.Add(vehicle.lookupCode);
            }
            LookUpCodesViewModel viewModel = new LookUpCodesViewModel(lookupCodes);
            if (viewModel == null)
            {
                return View("Error");
            }
            return View(lookupCodes);
        }

    }
}
