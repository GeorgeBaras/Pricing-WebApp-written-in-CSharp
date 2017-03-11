using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;
using Tutorial.MyClasses;

namespace Tutorial.My_XMLClasses
{
    public class XMLManager
    {
        public static string serializeAndReturnXMLPath(Object obj) {
            var serializer = new XmlSerializer(obj.GetType());

            StreamWriter writer = new StreamWriter(obj.GetType().ToString()+ obj.ToString() + ".xml");

            serializer.Serialize(writer, obj);

            string writerPath = ((FileStream)(writer.BaseStream)).Name;
            
            writer.Close();
            
            return writerPath;
        }


        public static T Deserialize<T>(string xml, string defaultNamespace)
        {
            var serializer = new XmlSerializer(typeof(T), defaultNamespace);
            object obj;
            using (var stringReader = new StringReader(xml))
            {
                obj = serializer.Deserialize(stringReader);
                stringReader.Close();
            }
            return (T)obj;
        }



        public static Object deserialize(string fullPath) {
            var serializer = new XmlSerializer(typeof(Object));
            if (File.Exists(fullPath))
            {
                Stream stream = File.OpenRead(fullPath);
                return (Object)serializer.Deserialize(stream);
            }
            return null;
        }

        public static Object deserializeVehicle(string fullPath)
        {
            var streamReader = new StreamReader(fullPath);
            string myXml = streamReader.ReadToEnd();

            XDocument xdocument = new XDocument();
            xdocument = XDocument.Parse(myXml);

            var vehicle = xdocument.Element("vehicle");

            string id = vehicle.Attribute("id").Value;
            var elements = vehicle.Descendants();

            string make = vehicle.Element("make").Value;
            string model = vehicle.Element("model").Value;
            string derivative = vehicle.Element("derivative").Value;
            string lookupCode = vehicle.Element("lookupCode").Value;
            int mileage = Convert.ToInt32(vehicle.Element("mileage").Value);
            decimal value = Convert.ToDecimal(vehicle.Element("value").Value);

            Vehicle vehicleInstance = new Vehicle(make, model, derivative, lookupCode, mileage, value);
            return vehicleInstance;

        }
    }
}