using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.My_XMLClasses;
using Tutorial.MyClasses;
using System.IO;
using System.Reflection;

namespace Tutorial.Tests.MyXMLClassesUT
{
    [TestClass]
    public class TestMarsharllingUnmarshalling
    {
        [TestMethod]
        public void TestSerialize()
        {
            Vehicle vehicle = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));
            PriceBand priceBand = new PriceBand(10, new decimal(10000));
            PriceBand priceBand1 = new PriceBand(20, new decimal(20000));

            PriceRecord priceRecord = new PriceRecord("prLookUp", priceBand);
            priceRecord.PriceBands.Add(priceBand1);
            
            Assert.IsTrue(File.Exists(XMLManager.serializeAndReturnXMLPath(vehicle)),"Vehicle Xml file was created successfully");
            Assert.IsTrue(File.Exists(XMLManager.serializeAndReturnXMLPath(priceBand)), "PriceBand Xml file was created successfully");
            Assert.IsTrue(File.Exists(XMLManager.serializeAndReturnXMLPath(priceRecord)), "PriceRecord Xml file was created successfully");

        }

        [TestMethod]
        public void TestDeserializeVehicle()
        {
            Vehicle vehicle = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));

            string xmlFilePath = XMLManager.serializeAndReturnXMLPath(vehicle);

            Vehicle unmarshalledVehicle = (Vehicle)XMLManager.deserializeVehicle(xmlFilePath);

            Assert.IsTrue(vehicle.Equals(unmarshalledVehicle),"Unmarshalled vehicle was not same as the original one.");
        }

        [TestMethod]
        public void TestDeserializePriceBand()
        {
            PriceBand priceBand = new PriceBand(10, new decimal(10000));

            string xmlFilePath = XMLManager.serializeAndReturnXMLPath(priceBand);

            PriceBand unmarshalledPriceBand = XMLManager.deserializePriceBand(xmlFilePath);

            Assert.IsTrue(priceBand.Equals(unmarshalledPriceBand), "Unmarshalled priceBand was not same as the original one.");
        }

        [TestMethod]
        public void TestDeserializePriceRecord()
        {
            PriceBand priceBand = new PriceBand(10, new decimal(10000));
            PriceBand priceBand1 = new PriceBand(20, new decimal(20000));

            PriceRecord priceRecord = new PriceRecord("prLookUp", priceBand);
            priceRecord.PriceBands.Add(priceBand1);

            string xmlFilePath = XMLManager.serializeAndReturnXMLPath(priceRecord);

            PriceRecord unmarshalledPriceRecord = XMLManager.deserializePriceRecord(xmlFilePath);

            Assert.IsTrue(priceRecord.Equals(unmarshalledPriceRecord), "Unmarshalled priceRecord was not same as the original one.");
        }
    }
}
