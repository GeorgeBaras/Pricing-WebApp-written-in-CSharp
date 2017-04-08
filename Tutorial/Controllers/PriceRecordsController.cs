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
        public ActionResult PriceRecordsIndex()
        {
            PriceRecordList priceRecordList = new PriceRecordList();
            priceRecordList.priceRecordlist = MasterRepository.priceRecordRepository.getAllEntries();
            if (priceRecordList.priceRecordlist.Count < 1) {
                return View("Error");
            }
            return View(priceRecordList);
        }

        [HttpPost]
        public ActionResult AddPriceRecord(string lookupCode) {
            return View();
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
            MasterRepository.vehicleRepository.updateFieldBylookupCode(oldLookupCode, VehicleDAO.VehicleFields.lookupCode, updatedLookupCode);
            if (priceRecord == null)
            {
                return View("Error");
            }
            PriceRecordList priceRecordList = new PriceRecordList();
            priceRecordList.priceRecordlist = MasterRepository.priceRecordRepository.getAllEntries();
            if (priceRecordList.priceRecordlist.Count < 1)
            {
                return View("Error");
            }
            return View("PriceRecordsIndex", priceRecordList);
        }


        // POST: PriceBands/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PriceBands/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PriceBands/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
