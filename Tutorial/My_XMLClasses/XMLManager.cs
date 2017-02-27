using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Tutorial.My_XMLClasses
{
    public class XMLManager
    {
        public static string serializeAndReturnXMLPath(StreamWriter writer, Object obj) {
            var serializer = new XmlSerializer(obj.GetType());
            serializer.Serialize(writer, obj);
            string writerPath = ((FileStream)(writer.BaseStream)).Name;
            return writerPath;
        }


        public static Object deserialize(string fullPath) {
            var serializer = new XmlSerializer(typeof(Object));
            if (File.Exists(fullPath))
            {
                Stream stream = File.OpenRead(fullPath);
                return serializer.Deserialize(stream) as Object;
            }
            return null;
        }
    }
}