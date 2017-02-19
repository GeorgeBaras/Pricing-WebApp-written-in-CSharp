using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    //[Table("PriceRecords")]
    public class PriceRecord : IPriceRecord
    {
        public int PriceRecordId { get; set; }
        public String LookupCode { get; set; }
        public virtual List<PriceBand> PriceBands { get; set; }
        public PriceRecord() {
            PriceBands = new List<PriceBand>();
        }
        public PriceRecord(String lookupCode, List<PriceBand> priceBands) {
            PriceBands = new List<PriceBand>();
            LookupCode = lookupCode;
            PriceBands = priceBands;
        }

        public PriceRecord(String lookupCode, PriceBand priceBand)
        {
            PriceBands = new List<PriceBand>();
            LookupCode = lookupCode;
            PriceBands.Add(priceBand);
        }

        public string getLookupCode()
        {
            return LookupCode;
        }

        public List<PriceBand> getPriceBands()
        {
            return PriceBands;
        }
    }
}