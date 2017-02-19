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
            //using (var ctx = (MyApplicationDBContext) myContainer.Resolve<DbContext>("applicationDBContext"))
            using (var ctx = new MyApplicationDBContext())
            {
                Vehicle vehicle = new Vehicle("myMake", "myModel", "myDerivative", "myLookupCode", 10, new decimal(20000));
                ctx.Vehicles.Add(vehicle);
                ctx.SaveChanges();

                PriceBand priceBand1 = new PriceBand(10, new decimal(1000));
                PriceBand priceBand2 = new PriceBand(20, new decimal(2000));
                PriceRecord priceRecord = new PriceRecord("firstRecord", priceBand1);
                priceRecord.PriceBands.Add(priceBand2);

                ctx.PriceRecords.Add((PriceRecord)priceRecord);
                ctx.SaveChanges();


            }
            
        }
        [Ignore]
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
