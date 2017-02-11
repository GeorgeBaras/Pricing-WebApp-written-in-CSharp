using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.MyClasses;
using System.Collections.Generic;
using Tutorial.EF_DBContext;

namespace Tutorial.Tests.DataBaseUT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod()
        {
            using (var ctx = new MyApplicationDBContext())
            {
                Vehicle vehicle = new Vehicle("myMake", "myModel", "myDerivative", "myLookupCode", 10, new decimal(20000));
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                PriceBandImp priceBand = new PriceBandImp(20, new decimal(20000));
                ctx.PriceBands.Add(priceBand);
                ctx.SaveChanges();

                PriceBandImp priceBand1 = new PriceBandImp(30, new decimal(4000));
                ctx.PriceBands.Add(priceBand1);
                ctx.SaveChanges();

                PriceRecordImp priceRecord = new PriceRecordImp("testPR", priceBand);
                priceRecord.priceBands.Add(priceBand1);

                ctx.PriceRecords.Add(priceRecord);
                ctx.SaveChanges();

            }

            //VehicleRepository vehicleRepository = new VehicleRepository();
            //List<Vehicle> vehicles = vehicleRepository.GetVehicles();
            //Assert.AreEqual(0, vehicles.Capacity, "Incorrect number of vehicles returned");
        }
    }
}
