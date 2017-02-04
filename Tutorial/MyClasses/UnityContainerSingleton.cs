using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            myUnityContainer.RegisterType<PriceBand, PriceBandImp>("lowBand", new InjectionConstructor(10, new Decimal(20000.0)));
            myUnityContainer.RegisterType<PriceBand, PriceBandImp>("midBand", new InjectionConstructor(15, new Decimal(15000.0)));
            myUnityContainer.RegisterType<PriceBand, PriceBandImp>("highBand", new InjectionConstructor(20, new Decimal(10000.0)));

            // PriceRecords
            List<PriceBand> allBands = new List<PriceBand>();
            allBands.Add(myUnityContainer.Resolve<PriceBand>("lowBand"));
            allBands.Add(myUnityContainer.Resolve<PriceBand>("midBand"));
            allBands.Add(myUnityContainer.Resolve<PriceBand>("highBand"));

            myUnityContainer.RegisterType<PriceRecord, PriceRecordImp>("lowOnly", new InjectionConstructor("lowOnly", allBands.GetRange(0, 1)));
            myUnityContainer.RegisterType<PriceRecord, PriceRecordImp>("lowAndMid", new InjectionConstructor("lowAndMid", allBands.GetRange(0,2)));
            myUnityContainer.RegisterType<PriceRecord, PriceRecordImp>("allBands", new InjectionConstructor("allBands", allBands));

            // ValuatioDAO prerequisite list and dictionary
            List<PriceRecord> priceRecordsList = new List<PriceRecord>();
            priceRecordsList.Add(myUnityContainer.Resolve<PriceRecord>("lowOnly"));
            priceRecordsList.Add(myUnityContainer.Resolve<PriceRecord>("lowAndMid"));
            priceRecordsList.Add(myUnityContainer.Resolve<PriceRecord>("allBands"));

            Dictionary<String, PriceRecord> priceRecordsDictionary = new Dictionary<string, PriceRecord>();
            priceRecordsDictionary.Add("lowOnly", myUnityContainer.Resolve<PriceRecord>("lowOnly"));
            priceRecordsDictionary.Add("lowAndMid", myUnityContainer.Resolve<PriceRecord>("lowAndMid"));
            priceRecordsDictionary.Add("allBands", myUnityContainer.Resolve<PriceRecord>("allBands"));

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



        }

        public static UnityContainerSingleton getInstance() {
            return instance;
        }

        public static IUnityContainer getContainer() {
            return myUnityContainer;
        }
    }
}