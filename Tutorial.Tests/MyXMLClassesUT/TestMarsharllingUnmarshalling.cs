using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tutorial.My_XMLClasses;
using Tutorial.MyClasses;
using System.IO;

namespace Tutorial.Tests.MyXMLClassesUT
{
    [TestClass]
    public class TestMarsharllingUnmarshalling
    {
        [TestMethod]
        public void TestSerialize()
        {
            Vehicle vehicle = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));

            string type = vehicle.GetType().ToString();
            StreamWriter writer = new StreamWriter(vehicle.GetType().ToString()+vehicle.id.ToString()+".xml");

            Assert.IsTrue(File.Exists(XMLManager.serializeAndReturnXMLPath(writer, vehicle)),"Xml file was created successfully");
        }

        [TestMethod]
        public void TestDeserialize()
        {
            // create xml file
            Vehicle vehicle = new Vehicle("myMake100", "myModel100", "myDerivative100", "myLookupCode100", 10, new decimal(10000));
            string type = vehicle.GetType().ToString();
            StreamWriter writer = new StreamWriter(vehicle.GetType().ToString() + vehicle.id.ToString() + ".xml");
            string xmlFilePath = XMLManager.serializeAndReturnXMLPath(writer, vehicle);

            writer.Close();

            // unmarshal vehicle object from xml file
            Vehicle unmarshalledVehicle = (Vehicle)XMLManager.deserialize(xmlFilePath);

            Assert.IsTrue(Vehicle.Equals(vehicle, unmarshalledVehicle),"Unmarshalled vehicle was not same as the original one.");
        }
    }
}
