using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext_Repositories
{
    public class PriceRecordDAO : IDao
    {
        public enum PriceRecordFields { lookupCode, priceBands}

        private appDBContext db;
        public PriceRecordDAO(appDBContext db)
        {
            this.db = db;
        }

        public PriceRecord addPriceRecord(PriceRecord priceRecord) {
            PriceRecord addedPriceRecord = db.PriceRecords.Add(priceRecord);
            db.SaveChanges();
            return addedPriceRecord;
        }

        public int addPriceRecordList(List<PriceRecord> priceRecords)
        {
            int addedPriceRecords = 0;
            foreach (PriceRecord priceRecord in priceRecords)
            {
                if (addPriceRecord(priceRecord) != null)
                {
                    addedPriceRecords++;
                }
            }
            return addedPriceRecords;
        }

        public List<PriceRecord> getAllEntries()
        {
            return db.PriceRecords.ToList();
        }

        public PriceRecord getPriceRecordByLookupCode(String lookupCode)
        {
            var query = from v in db.PriceRecords
                        orderby v.LookupCode
                        select v;

            foreach (PriceRecord priceRecord in query)
            {
                if (priceRecord.LookupCode.Equals(lookupCode))
                {
                    return priceRecord;
                }
            }
            return null;
        }

        public Boolean deletePriceRecordByLookupCode(string lookupCode)
        {
            int deleted = db.Database.ExecuteSqlCommand("DELETE FROM dbo.PriceRecords WHERE LookupCode =@lookupCode", new SqlParameter("@lookupCode",lookupCode));
            return deleted == 1;
        }


        public Boolean updateLookupCode(string lookupCode, string newLookupCode) {
            PriceRecord priceRecord = getPriceRecordByLookupCode(lookupCode);

            priceRecord.LookupCode = newLookupCode.ToString();
            db.SaveChanges();
            if (priceRecord.LookupCode.Equals(newLookupCode)){
                return true;
            }
            return false;
        }

        public Boolean updatePriceBands(string lookupCode, List<PriceBand> updatedPriceBands)
        {
            PriceRecord priceRecord = getPriceRecordByLookupCode(lookupCode);
            List<PriceBand> priceBands = getPriceBandsByPriceRecordId(priceRecord.PriceRecordId);
            db.PriceBands.RemoveRange(priceBands);
            db.SaveChanges();

            foreach (var priceBand in updatedPriceBands)
            {
                priceBand.PriceRecordId = priceRecord.PriceRecordId;
                db.PriceBands.Add(priceBand);
            }

            db.SaveChanges();
            if (priceRecord.PriceBands.Count == updatedPriceBands.Count)
            {
                return true;
            }
            return false;
        }


        public bool dbIsEmpty()
        {
            return getAllEntries().Count == 0;
        }

        public bool deleteAllEntries()
        {
            //var query = from p in db.PriceRecords
            //            select p;
            //foreach (var priceRecord in query)
            //{
            //    if (!deletePriceRecordByLookupCode(priceRecord.LookupCode)){
            //        return false;
            //    }
            //}
            //return true;                     
            int deleted = db.Database.ExecuteSqlCommand("DELETE FROM dbo.PriceRecords");
            return deleted == 1;
        }

        private List<PriceBand> getPriceBandsByPriceRecordId(int priceRecordId) {
            List<PriceBand> priceBands = new List<PriceBand>();
            var query = from v in db.PriceBands
                            where v.PriceRecordId == priceRecordId
                            orderby v.PriceBandId
                            select v;

            foreach (var priceBand in query)
            {
                priceBands.Add(priceBand);
            }
            return priceBands;
        }
    }
}