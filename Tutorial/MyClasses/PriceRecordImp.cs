using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class PriceRecordImp : PriceRecord
    {
        private String lookupCode;
        public List<PriceBand> priceBands;

        public PriceRecordImp(String lookupCode, List<PriceBand> priceBands) {
            this.lookupCode = lookupCode;
            this.priceBands = priceBands;
        }

        public PriceRecordImp(String lookupCode, PriceBand priceBand)
        {
            this.lookupCode = lookupCode;
            this.priceBands.Add(priceBand);
        }

        public string getLookupCode()
        {
            return this.lookupCode;
        }

        public List<PriceBand> getPriceBands()
        {
            return this.priceBands;
        }
    }
}