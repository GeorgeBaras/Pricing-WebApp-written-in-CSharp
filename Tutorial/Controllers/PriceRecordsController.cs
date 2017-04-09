using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.Models;
using Tutorial.My_EntityClasses;
using Tutorial.MyClasses;

namespace Tutorial.Controllers
{
    public class PriceRecordsController : Controller
    {

        // GET: PriceBands
        public ActionResult PriceRecordsIndex(int page=1)
        {
            int pageSize;
            PriceRecordList priceRecordList;
            List<PriceRecord> allRecords;
            getPaginatedPriceRecordList(page, out pageSize, out priceRecordList, out allRecords);

            if (priceRecordList.priceRecordlist.Count < 1)
            {
                return View("Error");
            }
            ViewData["pages"] = Math.Ceiling((double)allRecords.Count / pageSize);
            return View(priceRecordList);
        }

        private static void getPaginatedPriceRecordList(int page, out int pageSize, out PriceRecordList priceRecordList, out List<PriceRecord> allRecords)
        {
            pageSize = 4;
            priceRecordList = new PriceRecordList();
            allRecords = MasterRepository.priceRecordRepository.getAllEntries();
            // Get only the priceRecords for the specific page

            for (int i = (page - 1) * pageSize; i <= (page * pageSize) - 1; i++)
            {
                if (i > allRecords.Count - 1)
                {
                    break;
                }
                priceRecordList.priceRecordlist.Add(allRecords[i]);
            }
        }

        

        [HttpPost]
        public ActionResult EditPriceRecord(string lookupCode)
        {
            PriceRecord priceRecord= MasterRepository.priceRecordRepository.getPriceRecordByLookupCode(lookupCode);
            if (priceRecord == null)
            {
                return View("Error");
            }
            return View("EditPriceRecord", priceRecord);
        }

        [HttpPost]
        public ActionResult SaveEditedPriceRecord(FormCollection form)
        {
            String oldLookupCode = Request.Form["oldLookupCode"];
            String updatedLookupCode = Request.Form["LookupCode"];
            PriceRecord priceRecord = MasterRepository.priceRecordRepository.getPriceRecordByLookupCode(oldLookupCode);
            MasterRepository.priceRecordRepository.updateLookupCode(oldLookupCode, updatedLookupCode);

            if (lookupCodeExistInVehicles(oldLookupCode))
            {
                MasterRepository.vehicleRepository.updateFieldBylookupCode(oldLookupCode, VehicleDAO.VehicleFields.lookupCode, updatedLookupCode);
            }


            //if (priceRecord == null)
            //{
            //    return View("Error");
            //}
            //PriceRecordList priceRecordList = new PriceRecordList();
            //priceRecordList.priceRecordlist = MasterRepository.priceRecordRepository.getAllEntries();
            //if (priceRecordList.priceRecordlist.Count < 1)
            //{
            //    return View("Error");
            //}
            //return View("PriceRecordsIndex", priceRecordList);


            int pageSize;
            PriceRecordList priceRecordList;
            List<PriceRecord> allRecords;
            getPaginatedPriceRecordList(1, out pageSize, out priceRecordList, out allRecords);

            if (priceRecordList.priceRecordlist.Count < 1)
            {
                return View("Error");
            }
            ViewData["pages"] = Math.Ceiling((double)allRecords.Count / pageSize);
            return View("PriceRecordsIndex", priceRecordList);
        }


        public ActionResult AddPriceRecord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPriceRecord(FormCollection form)
        {
            string priceRecordName = Request.Form["PriceRecordName"];
            string pb1mileage = Request.Form["pb1mileage"];
            string pb1valuation = Request.Form["pb1valuation"];
            string pb2mileage = Request.Form["pb2mileage"];
            string pb2valuation = Request.Form["pb2valuation"];

            PriceBand priceBand1 = new PriceBand(Convert.ToInt16(pb1mileage), Convert.ToDecimal(pb1valuation));
            PriceBand priceBand2 = new PriceBand(Convert.ToInt16(pb2mileage), Convert.ToDecimal(pb2valuation));

            PriceRecord priceRecord = new PriceRecord(priceRecordName, priceBand1);
            priceRecord.PriceBands.Add(priceBand2);
            int entriesBeforeAddition = MasterRepository.priceRecordRepository.getAllEntries().Count;
            MasterRepository.priceRecordRepository.addPriceRecord(priceRecord);

            if (entriesBeforeAddition >= MasterRepository.priceRecordRepository.getAllEntries().Count) {
                return View("Error");
            }
            return View("RecordAdded");
        }

        private bool lookupCodeExistInVehicles(string lookupCode) {
            VehicleList vehicleList = new VehicleList(MasterRepository.vehicleRepository.getAllEntries());
            foreach (var vehicle in vehicleList.vehicleList)
            {
                if (vehicle.lookupCode.Equals(lookupCode)){
                    return true;
                }
            }
            return false;
        }

    }
}
