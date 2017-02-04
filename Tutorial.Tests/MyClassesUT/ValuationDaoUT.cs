using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Tutorial.MyClasses;

namespace Tutorial.Tests.MyClassesUT
{
    [TestClass]
    public class ValuationDaoUT
    {
        IUnityContainer myContainer;

        [TestMethod]
        public void TestAddPriceRecord()
        {
            myContainer = UnityContainerSingleton.getContainer();

            /*
            PriceBandImp priceBand = (PriceBandImp) myContainer.Resolve<PriceBand>("lowBand");

            PriceRecordImp priceRecord = (PriceRecordImp)myContainer.Resolve<PriceRecord>("lowAndMid");

            int actualMileage = priceBand.getMileage();
            int expectedMileage = 10;
            Assert.AreEqual(expectedMileage, actualMileage);

            PriceBandImp anotherPriceBand = (PriceBandImp) priceRecord.getPriceBands()[0];
            Assert.AreEqual(priceBand.getMileage(), anotherPriceBand.getMileage());


            ValuationDAOListImp valuationDAOList = (ValuationDAOListImp) myContainer.Resolve<ValuationDAO>("ValuationDAOwithList");
            int mileage = valuationDAOList.getPriceRecord("lowOnly").getPriceBands()[0].getMileage();
            Assert.AreEqual(mileage, 10);

            CAPValuationCalculator calculator = (CAPValuationCalculator)myContainer.Resolve<ValuationCalculator>("CapValuationCalculator");

            Assert.AreEqual(calculator.calculate(valuationDAOList.getPriceRecord("lowOnly"), 10), new decimal(20000.0));
            */

            // valuationService.ValuationDAO = myContainer.Resolve<ValuationDAO>("ValuationDAOwithList");
            // valuationService.ValuationCalculator = myContainer.Resolve<ValuationCalculator>("CapValuationCalculator");

            ValuationServiceImp valuationService = (ValuationServiceImp)myContainer.Resolve<ValuationService>("ValuationServiceWithDAOList");
            Vehicle vehicle = new Vehicle("make", "model", "derivative", "lowOnly", 10, new decimal(20000.0));

            decimal actualValuation = valuationService.valueVehicle(vehicle);
            decimal expectedValuation = new decimal(20000.0);

            Assert.AreEqual(expectedValuation, actualValuation);

        }
    }
}
