using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.MyClasses;
using Moq;

namespace Tutorial.Tests.MyClassesUT
{
    /// <summary>
    /// Summary description for ValuationServiceTest
    /// </summary>
    [TestClass]
    public class ValuationServiceTest
    {
        private Vehicle vehicle;
        private ValuationServiceImp valuationService;
        private Mock<ValuationDAO> mockValuationDAO;
        private Mock<ValuationCalculator> mockValuationCalculator;
        private PriceRecord priceRecord;

        [TestInitialize]
        public void setUp() {
            vehicle = new Vehicle("make", "model", "derivative", "lookupCode", 10, new decimal(1000));
            // mock ValuationService
            valuationService = new ValuationServiceImp();

            // mock ValuationDAO
            mockValuationDAO = new Mock<ValuationDAO>();
            // mock ValuationCalculator
            mockValuationCalculator = new Mock<ValuationCalculator>();
            
            priceRecord = new PriceRecordImp("lookupCode", new List<PriceBand>());

            mockValuationDAO
                .Setup(o => o.getPriceRecord(It.IsAny<String>()))
                .Returns(priceRecord);

            mockValuationCalculator
                .Setup(o => o.calculate(priceRecord, 10))
                .Returns(new decimal(1000));

            // Set the properties of the valuationService to be the mocked ones
            this.valuationService.ValuationDAO = mockValuationDAO.Object;
            valuationService.ValuationCalculator = mockValuationCalculator.Object;

        }

        [TestMethod]
        public void testValueVehicle()
        {
            decimal expected = new decimal(1000);
            decimal actual = valuationService.valueVehicle(vehicle);

            // Verify that the calls to the mocks took place
            mockValuationCalculator.Verify(o => o.calculate(It.IsAny<PriceRecord>(), It.IsAny<int>()), Times.AtLeast(1));
            mockValuationDAO.Verify(o => o.getPriceRecord(It.IsAny<String>()), Times.Once);
            // Assert the the mocks returned the correct values
            Assert.AreEqual(expected, actual, "Values did not match");
                
         }
    }
}
