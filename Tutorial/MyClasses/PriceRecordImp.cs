using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    [Table("PriceRecords")]
    public class PriceRecordImp : PriceRecord
    {
        public int id { get; set; }
        [Required]
        public String lookupCode { get; set; }
        public List<PriceBand> priceBands { get; set; }
        public PriceRecordImp() {
            this.priceBands = new List<PriceBand>();
        }
        public PriceRecordImp(String lookupCode, List<PriceBand> priceBands) {
            this.priceBands = new List<PriceBand>();
            this.lookupCode = lookupCode;
            this.priceBands = priceBands;
        }

        public PriceRecordImp(String lookupCode, PriceBand priceBand)
        {
            this.priceBands = new List<PriceBand>();
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