using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial.MyClasses
{
    public class ValuationServiceImp : ValuationService
    {
        public ValuationDAO valuationDAO;
        public ValuationCalculator valuationCalculator;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ValuationServiceImp() { }

        public ValuationServiceImp(ValuationDAO valuationDAO, ValuationCalculator valuationCalculator) {
            this.ValuationDAO = valuationDAO;
            this.ValuationCalculator = valuationCalculator;
        }

        public decimal valueVehicle(Vehicle vehicle)
        {
            log.Info("Got into the vehicle valuation for vehicle-lookupCode: "+vehicle.lookupCode);
            return this.ValuationCalculator.calculate(this.ValuationDAO.getPriceRecord(vehicle.lookupCode),vehicle.mileage);
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