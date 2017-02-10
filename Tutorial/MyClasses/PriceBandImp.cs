using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class PriceBandImp : PriceBand
    {
        private int mileage;
        private decimal valuation;

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