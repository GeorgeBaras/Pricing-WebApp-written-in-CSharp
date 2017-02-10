using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class PriceBandImp : PriceBand
    {
        public virtual int mileage { get; set; }
        public virtual decimal valuation{ get; set; }

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