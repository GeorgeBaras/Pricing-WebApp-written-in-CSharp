using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class ValuationServiceImp : ValuationService
    {
        private ValuationDAO valuationDAO;
        private ValuationCalculator valuationCalculator;

        public decimal valueVehicle(Vehicle vehicle)
        {
            return this.ValuationCalculator.calculate(this.ValuationDAO.getPriceRecord(vehicle.LookupCode),vehicle.Mileage);
        }

        public ValuationDAO ValuationDAO
        {
            get
            {
                return valuationDAO;
            }

            set
            {
                valuationDAO = value;
            }
        }

        public ValuationCalculator ValuationCalculator
        {
            get
            {
                return valuationCalculator;
            }

            set
            {
                valuationCalculator = value;
            }
        }

    }
}