using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    [Table("PriceBands")]
    public class PriceBand : IPriceBand
    {
        public int PriceBandId { get; set; }
        public int Mileage { get; set; }
        public decimal Valuation { get; set; }

        public int PriceRecordImpId { get; set; }
        public virtual PriceRecord PriceRecordImp { get; set; }


        public PriceBand() { }
        public PriceBand(int mileage, decimal valuation) {
            this.Mileage = mileage;
            this.Valuation = valuation;
        }

        public int getMileage()
        {
            return this.Mileage;
        }

        public decimal getValuation()
        {
            return this.Valuation;
        }
    }
}