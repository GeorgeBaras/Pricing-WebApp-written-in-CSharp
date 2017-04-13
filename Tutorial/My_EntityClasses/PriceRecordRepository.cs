using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.MyClasses;

namespace Tutorial.EF_DBContext_Repositories
{
    public class PriceRecordRepository : IRepository
    {
        public enum PriceRecordFields { lookupCode, priceBands }

        private PriceRecordDAO priceRecordDAO;

        public PriceRecordRepository(PriceRecordDAO priceRecordDAO) {
            this.priceRecordDAO = priceRecordDAO;
        }

        public PriceRecord addPriceRecord(PriceRecord priceRecord)
        {
            return priceRecordDAO.addPriceRecord(priceRecord);
        }

        public int addPriceRecordList(List<PriceRecord> priceRecords)
        {
            return priceRecordDAO.addPriceRecordList(priceRecords);
        }

        public List<PriceRecord> getAllEntries()
        {
            return priceRecordDAO.getAllEntries();
        }

        public PriceRecord getPriceRecordByLookupCode(String lookupCode)
        {
            return priceRecordDAO.getPriceRecordByLookupCode(lookupCode);
        }

        public Boolean deletePriceRecordByLookupCode(string lookupCode)
        {
            return priceRecordDAO.deletePriceRecordByLookupCode(lookupCode);
        }

        public Boolean updateLookupCode(string lookupCode, string newLookupCode)
        {
            return priceRecordDAO.updateLookupCode(lookupCode, newLookupCode);
        }

        public Boolean updatePriceBands(string lookupCode, List<PriceBand> updatedPriceBands)
        {
            return priceRecordDAO.updatePriceBands(lookupCode, updatedPriceBands);
        }


        public bool dbIsEmpty()
        {
            return priceRecordDAO.dbIsEmpty();
        }

        public bool deleteAllEntries()
        {
            return priceRecordDAO.deleteAllEntries();
        }

        public int getCount() {
            return priceRecordDAO.getCount();
        }
    }
}