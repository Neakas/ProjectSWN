using System.IO;
using System.Xml.Linq;
using SWNAdmin.Controller;
using UniverseGeneration.Stellar_Bodies;

namespace SWNAdmin.Utility
{
    internal class XmlHandler
    {
        public static XElement CreateNode(string ElementName)
        {
            var xEle = new XElement(ElementName);
            return xEle;
        }

        public static XElement CreateNode(string ElementName, string Content)
        {
            var xEle = new XElement(ElementName, Content);
            return xEle;
        }

        public static string GrabXMLValue(XDocument Document, string Element)
        {
            var Value = "";
            foreach (var xEle in Document.Element("body").Elements())
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
            foreach (var xEle in Document.Element("body").Elements())
            {
                if (xEle.Name == Element)
                {
                    xEle.Value = Value;
                    Document.Save(SettingHandler.SettingPath());
                }
            }
            return Document;
        }

        public static void ExportSystemToXml(StarSystem System, string Path)
        {
            if (!File.Exists(Path))
            {
                File.Create(Path).Close();
            }
            var XDoc = new XDocument(new XElement("StarSystem", new XAttribute("Name", System.sysName)));
            XDoc.Save(Path);
        }
    }
}