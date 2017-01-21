using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.MyClasses;
using System.Collections.Generic;

namespace Tutorial.Tests.MyClassesUT
{
    [TestClass]
    public class CapValuationCalculatorUT
    {
        static PriceBand lowBand;
        static PriceBand midBand;
        static PriceBand highBand;
        static List<PriceBand> lowOnlyPriceBands;
        static List<PriceBand> lowAndMidPriceBands;
        static List<PriceBand> allBandsPriceBands;
        static PriceRecord lowOnly;
        static PriceRecord lowAndMid;
        static PriceRecord allBands;
        static CAPValuationCalculator capValuationCalculator;


        [ClassInitialize]
        public static void Setup(TestContext context) {
            lowBand = new PriceBandImp(10, new decimal(20000.0));
            midBand = new PriceBandImp(15, new decimal(15000.0));
            highBand = new PriceBandImp(20, new decimal(10000.0));

            lowOnlyPriceBands = new List<PriceBand>();
            lowOnlyPriceBands.Add(lowBand);
            lowOnly = new PriceRecordImp("lowBand", lowOnlyPriceBands);

            lowAndMidPriceBands = new List<PriceBand>();
            lowAndMidPriceBands.Add(lowBand);
            lowAndMidPriceBands.Add(midBand);
            lowAndMid = new PriceRecordImp("lowAndMid", lowAndMidPriceBands);

            allBandsPriceBands = new List<PriceBand>();
            allBandsPriceBands.Add(lowBand);
            allBandsPriceBands.Add(midBand);
            allBandsPriceBands.Add(highBand);
            allBands = new PriceRecordImp("allBands", allBandsPriceBands);

            capValuationCalculator = new CAPValuationCalculator();
        }

        [TestMethod]
        public void TestForExactPriceBandWithLowBandAnd15kMiles()
        {
            decimal actual = capValuationCalculator.calculate(lowAndMid, 15);
            decimal expected = new decimal(15000.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestForExactPriceBandWithAllBandsAnd15kMiles()
        {
            decimal actual = capValuationCalculator.calculate(allBands, 15);
            decimal expected = new decimal(15000.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestForExactPriceBandWithAllBandsAnd20kMiles()
        {
            decimal actual = capValuationCalculator.calculate(allBands, 20);
            decimal expected = new decimal(10000.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestBetweenTwoBandsWithLowAndMidAnd12kMiles()
        {
            decimal actual = capValuationCalculator.calculate(lowAndMid, 12);
            decimal expected = new decimal(18000.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestBetweenTwoBandsWithAllBandsAnd18kMiles()
        {
            decimal actual = capValuationCalculator.calculate(allBands, 18);
            decimal expected = new decimal(12000.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestBetweenTwoBandsWithAllBandsAnd13kMiles()
        {
            decimal actual = capValuationCalculator.calculate(allBands, 13);
            decimal expected = new decimal(17000.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestAboveMaxBandWithLowOnlyAnd20kMiles()
        {
            decimal actual = Math.Round(capValuationCalculator.calculate(lowOnly, 20), 2);
            decimal expected = new decimal(19408.04);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestAboveMaxBandWithLowAndMidAnd20kMiles()
        {
            decimal actual = Math.Round(capValuationCalculator.calculate(lowAndMid, 20), 2);
            decimal expected = new decimal(14776.35);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestBelowMinBandWithLowOnlyAnd9kMiles()
        {
            decimal actual = Math.Round(capValuationCalculator.calculate(lowOnly, 9), 2);
            decimal expected = new decimal(20060.0);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestBelowMinBandWithLowOnlyAnd8kMiles()
        {
            decimal actual = Math.Round(capValuationCalculator.calculate(lowOnly, 8), 2);
            decimal expected = new decimal(20120.18);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestFor1mmMiles()
        {
            decimal actual = Math.Round(capValuationCalculator.calculate(allBands, 1000), 2);
            decimal expected = new decimal(526.33);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }

        [TestMethod]
        public void TestForNewCustomBands()
        {
            PriceBand customLowBand = new PriceBandImp(1, new decimal(50000.0));
            PriceBand customHighBand = new PriceBandImp(10, new decimal(25000.0));
            List<PriceBand> customPriceBandList = new List<PriceBand>();
            customPriceBandList.Add(customLowBand);
            customPriceBandList.Add(customHighBand);

            PriceRecord customPR = new PriceRecordImp("customPR", customPriceBandList);

            decimal actual = Math.Round(capValuationCalculator.calculate(customPR, 2), 2);
            decimal expected = new decimal(47222.22);
            Assert.AreEqual<decimal>(expected, actual, "The capValuationCalculator miscalculated the price");
        }


    }
}
