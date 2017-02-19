using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tutorial.EF_DBContext;
using Tutorial.EF_DBContext_Repositories;

namespace Tutorial.MyClasses
{
    public class UnityContainerSingleton
    {
        private static UnityContainerSingleton instance = new UnityContainerSingleton();
        private static IUnityContainer myUnityContainer;

        private UnityContainerSingleton() {
            myUnityContainer = new UnityContainer();
            // All the configuration happens in the constructor

            // PriceBands
            myUnityContainer.RegisterType<IPriceBand, PriceBand>("lowBand", new InjectionConstructor(10, new Decimal(20000.0)));
            myUnityContainer.RegisterType<IPriceBand, PriceBand>("midBand", new InjectionConstructor(15, new Decimal(15000.0)));
            myUnityContainer.RegisterType<IPriceBand, PriceBand>("highBand", new InjectionConstructor(20, new Decimal(10000.0)));

            // PriceRecords
            List<PriceBand> allBands = new List<PriceBand> ();
            allBands.Add((PriceBand)myUnityContainer.Resolve<IPriceBand>("lowBand"));
            allBands.Add((PriceBand)myUnityContainer.Resolve<IPriceBand>("midBand"));
            allBands.Add((PriceBand)myUnityContainer.Resolve<IPriceBand>("highBand"));

            myUnityContainer.RegisterType<IPriceRecord, PriceRecord>("lowOnly", new InjectionConstructor("lowOnly", allBands.GetRange(0, 1)));
            myUnityContainer.RegisterType<IPriceRecord, PriceRecord>("lowAndMid", new InjectionConstructor("lowAndMid", allBands.GetRange(0,2)));
            myUnityContainer.RegisterType<IPriceRecord, PriceRecord>("allBands", new InjectionConstructor("allBands", allBands));

            // ValuatioDAO prerequisite list and dictionary
            List<PriceRecord> priceRecordsList = new List<PriceRecord>();
            priceRecordsList.Add((PriceRecord)myUnityContainer.Resolve<IPriceRecord>("lowOnly"));
            priceRecordsList.Add((PriceRecord)myUnityContainer.Resolve<IPriceRecord>("lowAndMid"));
            priceRecordsList.Add((PriceRecord)myUnityContainer.Resolve<IPriceRecord>("allBands"));

            Dictionary<String, PriceRecord> priceRecordsDictionary = new Dictionary<string, PriceRecord>();
            priceRecordsDictionary.Add("lowOnly", (PriceRecord) myUnityContainer.Resolve<IPriceRecord>("lowOnly"));
            priceRecordsDictionary.Add("lowAndMid", (PriceRecord)myUnityContainer.Resolve<IPriceRecord>("lowAndMid"));
            priceRecordsDictionary.Add("allBands", (PriceRecord)myUnityContainer.Resolve<IPriceRecord>("allBands"));

            // Register ValuationDAOList & ValuationDAODictionary

            myUnityContainer.RegisterType<ValuationDAO, ValuationDAOListImp>("ValuationDAOwithList", new InjectionConstructor(priceRecordsList));

            myUnityContainer.RegisterType<ValuationDAO, ValuationDAODictionaryImp>("ValuationDAOwithDictionary", new InjectionConstructor(priceRecordsDictionary));

            // ValuationCalculator
            myUnityContainer.RegisterType<ValuationCalculator, CAPValuationCalculator>("CapValuationCalculator");

            // ValuationService
            myUnityContainer.RegisterType<ValuationService, ValuationServiceImp>("ValuationServiceWithDAOList", new InjectionConstructor(
                myUnityContainer.Resolve<ValuationDAO>("ValuationDAOwithList"),
                myUnityContainer.Resolve<ValuationCalculator > ("CapValuationCalculator")
                ));

            myUnityContainer.RegisterType<ValuationService, ValuationServiceImp>("ValuationServiceWithDAODict", new InjectionConstructor(
                myUnityContainer.Resolve<ValuationDAO>("ValuationDAOwithDictionary"),
                myUnityContainer.Resolve<ValuationCalculator>("CapValuationCalculator")
                ));

            // MyApplicationDBContext
            myUnityContainer.RegisterType<DbContext, MyApplicationDBContext>("applicationDBContext");

            // Repositories
            myUnityContainer.RegisterType<IRepository, VehicleRepository>("vehicleRepository",new InjectionConstructor(myUnityContainer.Resolve<DbContext>("applicationDBContext")));
            myUnityContainer.RegisterType<IRepository, PriceBandRepository>("priceBandRepository", new InjectionConstructor(myUnityContainer.Resolve<DbContext>("applicationDBContext")));
            myUnityContainer.RegisterType<IRepository, PriceRecordRepository>("priceRecordRepository", new InjectionConstructor(myUnityContainer.Resolve<DbContext>("applicationDBContext")));


        }

        public static UnityContainerSingleton getInstance() {
            return instance;
        }

        public static IUnityContainer getContainer() {
            return myUnityContainer;
        }
    }
}