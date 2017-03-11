using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial.MyClasses;

namespace Tutorial.My_EntityClasses
{
   
    public class DatabaseGenerator
    {
        private static Random valuation = new Random();
        public static List<Vehicle> generateVehicles(int numberOfEntries) {
            List<Vehicle> vehicles = new List<Vehicle>();
            for (int i = 0; i < numberOfEntries; i++)
            {
                Vehicle vehicle = new Vehicle("myMake"+i.ToString(), "myModel" + i.ToString(), "myDerivative" + i.ToString(), "myLookupCode" + i.ToString(), i, new decimal(valuation.Next(1,100000)));
                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        public static List<PriceBand> generatePriceBands(int numberOfEntries)
        {
            List<PriceBand> priceBands = new List<PriceBand>();
            for (int i = 0; i < numberOfEntries; i++)
            {
                PriceBand priceBand = new PriceBand(i, new decimal(valuation.Next(1, 100000)));
                priceBands.Add(priceBand);
            }
            return priceBands;
        }

        public static List<PriceRecord> generatePriceRecords(int numberOfPriceRecordEntries, int numberOfPriceBandsPerPriceRecord)
        {
            List<PriceRecord> priceRecords = new List<PriceRecord>();
            for (int i = 0; i < numberOfPriceRecordEntries; i++)
            {
                PriceRecord priceRecord = new PriceRecord("prLookUp" + i.ToString(), generatePriceBands(numberOfPriceBandsPerPriceRecord));
                priceRecords.Add(priceRecord);
            }
            return priceRecords;
        }
        

    }
}