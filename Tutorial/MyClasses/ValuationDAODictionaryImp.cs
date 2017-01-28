using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses 
{
    public class ValuationDAODictionaryImp : ValuationDAO
    {
        private Dictionary<String, PriceRecord> priceRecordsDictionary { get; set; }

        public ValuationDAODictionaryImp(Dictionary<String, PriceRecord> priceRecordsDictionary)
        {
            this.priceRecordsDictionary = priceRecordsDictionary;
        }

        public PriceRecord getPriceRecord(string lookupCode)
        {
            if (priceRecordsDictionary.ContainsKey(lookupCode)){
                return this.priceRecordsDictionary[lookupCode];
            }
            else {
                return null;
            }
        }

        public void addPriceRecord(PriceRecord priceRecord) {
            this.priceRecordsDictionary.Add(priceRecord.getLookupCode(), priceRecord);
        }
}
}