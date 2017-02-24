using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;

namespace Tutorial.Tests.DataBaseUT
{
    [TestClass]
    public class PriceRecordRepositoryUT
    {
        [TestMethod]
        public void TestMethod1()
        {
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
