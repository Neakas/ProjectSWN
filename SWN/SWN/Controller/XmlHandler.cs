using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SWN
{
    class XmlHandler
    {
        public static XElement CreateNode(string ElementName)
        {
            XElement xEle = new XElement(ElementName);
            return xEle;
        }

        public static XElement CreateNode(string ElementName, string Content)
        {
            XElement xEle = new XElement(ElementName,Content);
            return xEle;
        }

        public static string GrabXMLValue(XDocument Document, string Element)
        {
            string Value = "";
            foreach (XElement xEle in Document.Element("configuration").Elements())
            {
                if (xEle.Name == Element)
                {
                    Value = xEle.Value;
                    return Value;
                }
            }
            return Value;
        }

        public static XDocument SetXmlValue(XDocument Document, string Element, string Value)
        {
            foreach (XElement xEle in Document.Element("configuration").Elements())
            {
                if (xEle.Name == Element)
                {
                    xEle.Value = Value;
                    Document.Save(SettingHandler.SettingPath());
                }
            }
            return Document;
        }

        public static void SerializeSystem(SWNServiceReference.StarSystems StarSystem)
        {
          
        // Create a new XmlSerializer instance with the type of the test class
        XmlSerializer SerializerObj = new XmlSerializer(typeof(SWNServiceReference.StarSystems));

                // Create a new file stream to write the serialized object to a file
                TextWriter WriteFileStream = new StreamWriter(@"C:\Test\test.xml");
                SerializerObj.Serialize(WriteFileStream, StarSystem);
 
        // Cleanup
        WriteFileStream.Close();
        }
    }
}
