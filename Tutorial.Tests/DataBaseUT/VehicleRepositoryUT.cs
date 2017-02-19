using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.MyClasses;
using System.Collections.Generic;
using Tutorial.EF_DBContext;
using System.Data.Entity;
using Microsoft.Practices.Unity;
using static Tutorial.EF_DBContext_Repositories.VehicleRepository;

namespace Tutorial.Tests.DataBaseUT
{

    [TestClass]
    public class VehicleRepositoryUT
    {
        
    static IUnityContainer  myContainer = UnityContainerSingleton.getContainer();
    static VehicleRepository vehicleRepository = (VehicleRepository) myContainer.Resolve<IRepository>("vehicleRepository");
    static appDBContext db = (appDBContext) myContainer.Resolve<DbContext>("applicationDBContext");

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContextInstance)
    {
      AddVehiclesToDB(db);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
      db.Database.Delete();
    }

        [TestMethod]
        public void TestAddVehicle() {
           Vehicle vehicle = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));
           Vehicle addedVehicle = vehicleRepository.addVehicle(vehicle);
           vehicleRepository.deleteVehicleBylookupCode("myLookupCode100");
           Assert.AreEqual(vehicle, addedVehicle, "Entry was not added correctly");
        
        }

        [Ignore]
        [TestMethod]
        public void TestAddVehicleList()
        {
            Vehicle vehicle1 = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));
            Vehicle vehicle2 = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));
            List<Vehicle> vehicleList = new List<Vehicle>();
            vehicleList.Add(vehicle1);
            vehicleList.Add(vehicle2);

            int countOfAddedVehicles = vehicleRepository.addVehicleList(vehicleList);
            int countAfterAddedVehicles = vehicleRepository.getAllEntries().Count;

            foreach (Vehicle vehicle in  vehicleList) {
                vehicleRepository.deleteVehicleBylookupCode(vehicle.lookupCode);
            }
            Assert.AreEqual(countAfterAddedVehicles- countOfAddedVehicles, vehicleRepository.getAllEntries().Count, "Entries were not added correctly");

        }

        [TestMethod]
        public void TestVehicleRepositoryGetAllEntries()
        {
            
            List<Vehicle> vehicles = vehicleRepository.getAllEntries();
            Assert.AreEqual(3, vehicles.Count, "Incorrect number of vehicles returned."); 
        }

        [TestMethod]
        public void TestVehicleRepositoryGetVehicleByLookupCode()
        {
            Vehicle vehicle = vehicleRepository.getVehicleByLookupCode("myLookupCode");
            Assert.AreEqual("myDerivative", vehicle.derivative, "Incorrect vehicle record retrieved");
        }

        [TestMethod]
        public void TestVehicleRepositoryUpdateFieldBylookupCode()
        {
            vehicleRepository.updateFieldBylookupCode<String>("myLookupCode", VehicleFields.make, "newAndUpdatedMake");
            Vehicle vehicle = vehicleRepository.getVehicleByLookupCode("myLookupCode");
            Assert.AreEqual("newAndUpdatedMake", vehicle.make, "Incorrect make for the updated entry");
        }


        [TestMethod]
        public void TestVehicleRepositoryDeleteVehicleBylookupCode()
        {
            vehicleRepository.deleteVehicleBylookupCode("myLookupCode2");
            Assert.AreEqual(2, vehicleRepository.getAllEntries().Count, "Vehicle was not removed");
        }


        [TestMethod]
        public void TestVehicleRepositoryDeleteAllEntries()
        {
            bool deleted = vehicleRepository.deleteAllEntries();
            // Recreate the DB entries
            AddVehiclesToDB((appDBContext)myContainer.Resolve<DbContext>("applicationDBContext"));

            Assert.IsTrue(deleted, "There are still entries in the DB");
            
        }

        [TestMethod]
        public void TestVehicleRepositoryDbIsEmpty()
        {
            Assert.IsTrue(!vehicleRepository.dbIsEmpty(), "Database is empty");
        }

        private static void AddVehiclesToDB(appDBContext db)
        {
            Vehicle vehicle = new Vehicle("myMake", "myModel", "myDerivative", "myLookupCode", 10, new decimal(10000));
            Vehicle vehicle1 = new Vehicle("myMake1", "myModel1", "myDerivative1", "myLookupCode1", 20, new decimal(20000));
            Vehicle vehicle2 = new Vehicle("myMake2", "myModel2", "myDerivative2", "myLookupCode2", 30, new decimal(30000));
            db.Vehicles.Add(vehicle);
            db.Vehicles.Add(vehicle1);
            db.Vehicles.Add(vehicle2);
            db.SaveChanges();
        }

        private static void AddPriceRecordsToDB(appDBContext db)
        {
            PriceBand priceBand1 = new PriceBand(10, new decimal(1000));
            PriceBand priceBand2 = new PriceBand(20, new decimal(2000));
            PriceBand priceBand3 = new PriceBand(20, new decimal(2000));

            PriceRecord priceRecord1 = new PriceRecord("firstRecord", priceBand1);
            PriceRecord priceRecord2 = new PriceRecord("secondRecord", priceBand1);

            priceRecord1.PriceBands.Add(priceBand1);

            priceRecord2.PriceBands.Add(priceBand1);
            priceRecord2.PriceBands.Add(priceBand2);
            priceRecord2.PriceBands.Add(priceBand3);


            db.PriceRecords.Add((PriceRecord)priceRecord1);
            db.PriceRecords.Add((PriceRecord)priceRecord2);
            db.SaveChanges();
        }


    }
}
