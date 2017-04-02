using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.EF_DBContext_Repositories;
using Tutorial.MyClasses;

namespace Tutorial.My_EntityClasses
{
   
    public class ModelWorld
    {
        private static Random valuation = new Random();

        private static List<String> brands = new List<String>() { "Audi", "Fiat", "Renault", "Ford" };
        private static List<String> models = new List<String>() { "sport", "compact", "sedan", "supermini" };

        static IUnityContainer myContainer = UnityContainerSingleton.getContainer();
        static VehicleRepository vehicleRepository = (VehicleRepository)myContainer.Resolve<IRepository>("vehicleRepository");
        static PriceRecordRepository priceRecordRepository = (PriceRecordRepository)myContainer.Resolve<IRepository>("priceRecordRepository");
        public static appDBContext db = (appDBContext)myContainer.Resolve<DbContext>("applicationDBContext");


        public static int GenerateSmallDB()
        {
            int numberOfRecords = 10;
            int priceBandsPerPriceRecord = 3;
            vehicleRepository.addVehicleList(ModelWorld.generateActualVehicles(numberOfRecords));
            priceRecordRepository.addPriceRecordList(ModelWorld.generatePriceRecords(numberOfRecords, priceBandsPerPriceRecord));
            return numberOfRecords;
        }

        public static List<Vehicle> generateActualVehicles(int numberOfEntries)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            for (int i = 0; i < numberOfEntries; i++)
            {
                Vehicle vehicle = new Vehicle(brands[valuation.Next(0, 3)], models[valuation.Next(0, 3)], "myDerivative" + i.ToString(), "prLookUp" + i.ToString(), valuation.Next(1, 100000), new decimal(valuation.Next(1, 100000)));
                vehicles.Add(vehicle);
            }
            return vehicles;
        }

        public static List<Vehicle> generateVehicles(int numberOfEntries) {
            List<Vehicle> vehicles = new List<Vehicle>();
            for (int i = 0; i < numberOfEntries; i++)
            {
                Vehicle vehicle = new Vehicle("myMake"+i.ToString(), "myModel" + i.ToString(), "myDerivative" + i.ToString(), "prLookUp" + i.ToString(), i, new decimal(valuation.Next(1,100000)));
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