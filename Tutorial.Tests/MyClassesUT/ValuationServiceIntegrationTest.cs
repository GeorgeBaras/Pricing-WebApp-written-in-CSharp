using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.MyClasses;
using Microsoft.Practices.Unity;

namespace Tutorial.Tests.MyClassesUT
{
    [TestClass]
    public class ValuationServiceIntegrationTest
    {
        IUnityContainer myContainer = UnityContainerSingleton.getContainer();

        [TestMethod]
        public void ValuationServiceWithListIntegrationTest()
        {
            ValuationServiceImp valuationService = (ValuationServiceImp)myContainer.Resolve<ValuationService>("ValuationServiceWithDAOList");
            Vehicle vehicle = new Vehicle("make", "model", "derivative", "lowOnly", 10, new decimal(20000.0));

            decimal actualValuation = valuationService.valueVehicle(vehicle);
            decimal expectedValuation = new decimal(20000.0);

            Assert.AreEqual(expectedValuation, actualValuation);

        }

        [TestMethod]
        public void ValuationServiceWithDictIntegrationTest()
        {
            ValuationServiceImp valuationService = (ValuationServiceImp) myContainer.Resolve<ValuationService>("ValuationServiceWithDAODict");
            Vehicle vehicle = new Vehicle("make", "model", "derivative", "lowOnly", 10, new decimal(20000.0));

            decimal actualValuation = valuationService.valueVehicle(vehicle);
            decimal expectedValuation = new decimal(20000.0);

            Assert.AreEqual(expectedValuation, actualValuation);
        }
    }
}
