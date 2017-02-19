using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Tutorial.MyClasses;
using System.Collections.Generic;

namespace Tutorial.Tests.MyClassesUT
{
    [TestClass]
    public class ValuationDaoUT
    {
        IUnityContainer myContainer = UnityContainerSingleton.getContainer();

        [TestMethod]
        public void ValuationDAOListUT()
        {
            ValuationDAOListImp valuationDAOList = (ValuationDAOListImp) myContainer.Resolve<ValuationDAO>("ValuationDAOwithList");
            int mileage = valuationDAOList.getPriceRecord("lowOnly").getPriceBands()[0].getMileage();
            Assert.AreEqual(mileage, 10);
        }

        [TestMethod]
        public void ValuationDAODictUT()
        {
            ValuationDAODictionaryImp valuationDAODict = (ValuationDAODictionaryImp)myContainer.Resolve<ValuationDAO>("ValuationDAOwithDictionary");
            int mileage = valuationDAODict.getPriceRecord("lowOnly").getPriceBands()[0].getMileage();
            Assert.AreEqual(mileage, 10);
        }
    }
}
