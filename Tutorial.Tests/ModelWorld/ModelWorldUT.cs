using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;
using Microsoft.Practices.Unity;
using Tutorial.EF_DBContext_Repositories;
using System.Data.Entity;
using Tutorial.My_EntityClasses;

namespace Tutorial.Tests.ModelWorld
{
    [TestClass]
    public class ModelWorldUT
    {
        static IUnityContainer myContainer = UnityContainerSingleton.getContainer();
        static VehicleRepository vehicleRepository = (VehicleRepository)myContainer.Resolve<IRepository>("vehicleRepository");
        static PriceRecordRepository priceRecordRepository = (PriceRecordRepository)myContainer.Resolve<IRepository>("priceRecordRepository");
        static appDBContext db = (appDBContext)myContainer.Resolve<DbContext>("applicationDBContext");


        [TestMethod]
        public void GenerateDatabaseSmallTestDB()
        {
            int numberOfRecords = My_EntityClasses.ModelWorld.GenerateSmallDB();

            Assert.AreEqual(numberOfRecords, vehicleRepository.getAllEntries().Count, "Not all vehicle entries were inserted in the database successfully");
            Assert.AreEqual(numberOfRecords, priceRecordRepository.getAllEntries().Count, "Not all priceRecord entries were inserted in the database successfully");
        }

       



        // [Ignore]
        [TestMethod]
        public void GenerateDatabase1k()
        {
            int numberOfRecords = 1000;
            int priceBandsPerPriceRecord = 3;
            vehicleRepository.addVehicleList(My_EntityClasses.ModelWorld.generateVehicles(numberOfRecords));
            priceRecordRepository.addPriceRecordList(My_EntityClasses.ModelWorld.generatePriceRecords(numberOfRecords, priceBandsPerPriceRecord));

            Assert.AreEqual(numberOfRecords, vehicleRepository.getAllEntries().Count, "Not all vehicle entries were inserted in the database successfully");
            Assert.AreEqual(numberOfRecords, priceRecordRepository.getAllEntries().Count, "Not all priceRecord entries were inserted in the database successfully");
        }

        [Ignore] // takes about 45'
        [TestMethod]
        public void GenerateDatabase10k()
        {
            int numberOfRecords = 10000;
            int priceBandsPerPriceRecord = 5;
            vehicleRepository.addVehicleList(My_EntityClasses.ModelWorld.generateVehicles(numberOfRecords));
            priceRecordRepository.addPriceRecordList(My_EntityClasses.ModelWorld.generatePriceRecords(numberOfRecords, priceBandsPerPriceRecord));

            Assert.AreEqual(numberOfRecords, vehicleRepository.getAllEntries().Count, "Not all vehicle entries were inserted in the database successfully");
            Assert.AreEqual(numberOfRecords, priceRecordRepository.getAllEntries().Count, "Not all priceRecord entries were inserted in the database successfully");
        }

        [Ignore]
        public void GenerateDatabase100k()
        {
            int numberOfRecords = 100000;
            int priceBandsPerPriceRecord = 10;
            vehicleRepository.addVehicleList(My_EntityClasses.ModelWorld.generateVehicles(numberOfRecords));
            priceRecordRepository.addPriceRecordList(My_EntityClasses.ModelWorld.generatePriceRecords(numberOfRecords, priceBandsPerPriceRecord));

            Assert.AreEqual(numberOfRecords, vehicleRepository.getAllEntries().Count, "Not all vehicle entries were inserted in the database successfully");
            Assert.AreEqual(numberOfRecords, priceRecordRepository.getAllEntries().Count, "Not all priceRecord entries were inserted in the database successfully");

        }
        [Ignore]
        [TestMethod]
        public void nukeDataBase() {
            db.Database.Delete();
            Assert.IsFalse(db.Database.Exists());
        }
    }
}
