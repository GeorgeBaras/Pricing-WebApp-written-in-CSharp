using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class ValuationDAOListImp : ValuationDAO
    {
        private List<PriceRecord> priceRecordsList { get; set; }

        public ValuationDAOListImp(List<PriceRecord> priceRecordsList) {
            this.priceRecordsList = priceRecordsList;
        }

        public PriceRecord getPriceRecord(string lookupCode)
        {
            foreach (PriceRecord priceRecord in priceRecordsList)
            {
                if (priceRecord.getLookupCode().Equals(lookupCode)) {
                    return priceRecord;
                }
            }
            return null;
        }

        public void addPriceRecord(PriceRecord priceRecord)
        {
            this.priceRecordsList.Add(priceRecord);
        }

        public String isOfType()
        {
            return "ValuationDAOListImp";
        }
    }
}