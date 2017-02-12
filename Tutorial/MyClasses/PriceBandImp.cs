using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    [Table("PriceBands")]
    public class PriceBandImp : PriceBand
    {
        public int id { get; set; }
        public int mileage { get; set; }
        public decimal valuation { get; set; }

        public PriceBandImp() { }
        public PriceBandImp(int mileage, decimal valuation) {
            this.mileage = mileage;
            this.valuation = valuation;
        }

        public int getMileage()
        {
            return this.mileage;
        }

        public decimal getValuation()
        {
            return this.valuation;
        }
    }
}