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
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public decimal valueVehicle(Vehicle vehicle)
        {
            log.Info("Got into the vehicle valuation for vehicle-lookupCode: "+vehicle.LookupCode);
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