using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class PriceRecordImp : PriceRecord
    {
        public int id { get; set; }
        public String lookupCode { get; set; }
        public ICollection<PriceBand> priceBands { get; set; }
        public PriceRecordImp() { }
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

        public ICollection<PriceBand> getPriceBands()
        {
            return this.priceBands;
        }
    }
}