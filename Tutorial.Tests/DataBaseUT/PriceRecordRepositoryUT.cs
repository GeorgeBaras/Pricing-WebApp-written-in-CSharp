using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;
using Microsoft.Practices.Unity;
using Tutorial.EF_DBContext_Repositories;
using System.Data.Entity;
using System.Collections.Generic;

namespace Tutorial.Tests.DataBaseUT
{
    [TestClass]
    public class PriceRecordRepositoryUT
    {
        static IUnityContainer myContainer = UnityContainerSingleton.getContainer();
        static PriceRecordRepository priceRecordRepository = (PriceRecordRepository) myContainer.Resolve<IRepository>("priceRecordRepository");
        static appDBContext db = (appDBContext)myContainer.Resolve<DbContext>("applicationDBContext");
        static int priceRecordEntriesCount;
        static int priceBandEntriesCount;


        [ClassInitialize]
        public static void ClassInitialize(TestContext testContextInstance)
        {
            db.Database.Delete();

            AddPriceRecordsToDB(db);
            priceRecordEntriesCount = 2;
            priceBandEntriesCount = 3;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            db.Database.Delete();
        }

        [TestMethod]
        public void TestAddPriceRecord()
        {
            PriceRecord priceRecord = getPriceRecord();

            if (priceRecordRepository.addPriceRecord(priceRecord) != null) {
                priceRecordEntriesCount++;
                priceBandEntriesCount+=3;
            }

            Assert.AreEqual(priceRecordRepository.getAllEntries().Count,priceRecordEntriesCount, "PriceRecord was not added properly");
        }

        [TestMethod]
        public void TestAddPriceRecordList()
        {
            PriceRecord priceRecord = getPriceRecord();
            PriceRecord priceRecord1 = getPriceRecord();
            if (priceRecordRepository.addPriceRecord(priceRecord) != null)
            {
                priceRecordEntriesCount++;
                priceBandEntriesCount += 3;
            }
            if (priceRecordRepository.addPriceRecord(priceRecord1) != null)
            {
                priceRecordEntriesCount++;
                priceBandEntriesCount += 3;
            }
            Assert.AreEqual(priceRecordRepository.getAllEntries().Count, priceRecordEntriesCount, "PriceRecords were not added properly");
        }


        [TestMethod]
        public void TestPriceRecordRepositoryGetAllEntries()
        {
            List<PriceRecord> priceRecords = priceRecordRepository.getAllEntries();
            Assert.AreEqual(priceRecordEntriesCount, priceRecords.Count, "Incorrect number of entries returned");
        }

        [TestMethod]
        public void TestGetPriceRecordByLookupCode()
        {
            PriceRecord priceRecord = priceRecordRepository.getPriceRecordByLookupCode("firstRecord");
            Assert.IsNotNull(priceRecord, "The record was not updated successfully");
            Assert.IsNotNull(priceRecord.getPriceBands());
            Assert.IsNotNull(priceRecord.getLookupCode());

        }

        [TestMethod]
        public void TestPriceRecordRepositoryUpdateLookupCode()
        {
            PriceRecord priceRecord = priceRecordRepository.getPriceRecordByLookupCode("secondRecord");
            priceRecordRepository.updateLookupCode("secondRecord", "updatedSecondRecord");
            Assert.IsNotNull(priceRecordRepository.getPriceRecordByLookupCode("updatedSecondRecord"), "The record was not updated successfully");
        }

        [TestMethod]
        public void TestPriceRecordRepositoryUpdatePriceBands()
        {
            PriceBand priceBand1 = new PriceBand(10000, new decimal(1));
            List<PriceBand> updatedPriceBands = new List<PriceBand>();
            updatedPriceBands.Add(priceBand1);

            List<PriceBand> priceBands = priceRecordRepository.getPriceRecordByLookupCode("firstRecord").getPriceBands();
            priceRecordRepository.updatePriceBands("firstRecord", updatedPriceBands);
            Assert.AreEqual(updatedPriceBands[0].getMileage(), priceRecordRepository.getPriceRecordByLookupCode("firstRecord").getPriceBands()[0].getMileage(), "PriceBands were not updated successfully");
        }

        [TestMethod]
        public void TestPriceRecordRepositoryDeletePriceRecordByLookupCode()
        {

            PriceRecord priceRecord = getPriceRecord();
            priceRecord.LookupCode = "testPRforDeletion";
            priceRecordRepository.addPriceRecord(priceRecord);

            bool deleted = priceRecordRepository.deletePriceRecordByLookupCode("testPRforDeletion");
            
            Assert.IsTrue(deleted, "Record not deleted successfully");
        }

        [Ignore]
        [TestMethod]
        public void TestPriceRecordRepositoryDeleteAllEntries()
        {
            int priceRecordsBeforeDeletion = priceRecordRepository.getAllEntries().Count;
            if (!priceRecordRepository.deleteAllEntries()) {
                Assert.Fail();
            }
            int priceRecordsAfterDeletion = priceRecordRepository.getAllEntries().Count;
            AddPriceRecordsToDB(db);
            priceRecordEntriesCount = 2;
            priceBandEntriesCount = 3;
            Assert.AreEqual(priceRecordEntriesCount, priceRecordsBeforeDeletion - priceRecordsAfterDeletion, "Deletion Failed");
        }

        [TestMethod]
        public void TestPriceRecordRepositoryDbIsEmpty()
        {
            Assert.IsFalse(priceRecordRepository.dbIsEmpty(), "DB is not empty");
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

        private PriceRecord getPriceRecord() {
            PriceBand priceBand1 = new PriceBand(10, new decimal(1000));
            PriceBand priceBand2 = new PriceBand(20, new decimal(2000));
            PriceBand priceBand3 = new PriceBand(20, new decimal(2000));

            PriceRecord priceRecord = new PriceRecord("testRecord", priceBand1);
            priceRecord.PriceBands.Add(priceBand1);
            priceRecord.PriceBands.Add(priceBand2);
            priceRecord.PriceBands.Add(priceBand3);

            return priceRecord;
        }
    }
}
