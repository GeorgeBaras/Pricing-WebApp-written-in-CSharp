using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.MyClasses;
using System.Collections.Generic;
using Tutorial.EF_DBContext;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace Tutorial.Tests.DataBaseUT
{
    [TestClass]
    public class UnitTest1
    {
        IUnityContainer myContainer = UnityContainerSingleton.getContainer();

        [TestMethod]
        public void TestMethod()
        {
            using (var ctx = (MyApplicationDBContext) myContainer.Resolve<DbContext>("applicationDBContext"))
            // new MyApplicationDBContext())
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

        [TestMethod]
        public void TestMethod1()
        {
            VehicleRepository vehicleRepository = new VehicleRepository();
            List<Vehicle> vehicles = vehicleRepository.GetVehicles();
            Assert.AreEqual(0, vehicles.Count, "Incorrect number of vehicles returned. It actually was "+ vehicles.Count+" and the first vehicle model was "
                +vehicles[0].model);
        
        }
    }
}
