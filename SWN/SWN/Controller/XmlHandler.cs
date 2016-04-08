using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using SWN.Service_References.SWNServiceReference;

namespace SWN.Controller
{
    internal class XmlHandler
    {
        public static XElement CreateNode(string elementName)
        {
            var xEle = new XElement(elementName);
            return xEle;
        }

        public static XElement CreateNode(string elementName, string content)
        {
            var xEle = new XElement(elementName, content);
            return xEle;
        }

        public static string GrabXmlValue(XDocument document, string element)
        {
            var xElement = document.Element("configuration");
            var firstOrDefault = (xElement?.Elements())?.FirstOrDefault(x => x.Name == element);
            var value = firstOrDefault?.Value;
            return value;
        }
        
        public static XDocument SetXmlValue(XDocument document, string element, string value)
        {
            var xElement = document.Element("configuration");
            var xEle = xElement?.Elements().FirstOrDefault(x => x.Name == element);
            if (xEle != null) xEle.Value = value;
            document.Save(SettingHandler.SettingPath());
            return document;
        }

        public static void SerializeSystem(StarSystems starSystem)
        {
            // Create a new XmlSerializer instance with the type of the test class
            var serializerObj = new XmlSerializer(typeof (StarSystems));
            try
            {
                // Create a new file stream to write the serialized object to a file
                TextWriter writeFileStream = new StreamWriter(@"C:\Test\test.xml");
                serializerObj.Serialize(writeFileStream, starSystem);
                writeFileStream.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}