using System.IO;
using System.Linq;
using System.Xml.Linq;
using SWNAdmin.Controller;
using UniverseGeneration.Stellar_Bodies;

namespace SWNAdmin.Utility
{
    internal static class XmlHandler
    {
        public static string GrabXmlValue( XDocument document, string element )
        {
            var value = "";
            var xElements = document?.Element("body")?.Elements().Where(xEle => xEle?.Name == element);
            if (xElements == null)
            {
                return value;
            }
            {
                foreach (var xEle in xElements)
                {
                    value = xEle.Value;
                    return value;
                }
            }
            return value;
        }

        public static XDocument SetXmlValue( XDocument document, string element, string value )
        {
            var xElements = document?.Element("body")?.Elements().Where(xEle => xEle.Name == element);
            if (xElements == null)
            {
                return document;
            }
            {
                foreach (var xEle in xElements)
                {
                    xEle.Value = value;
                    document.Save(SettingHandler.SettingPath());
                }
            }
            return document;
        }

        public static void ExportSystemToXml( StarSystem system, string path )
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            var xDoc = new XDocument(new XElement("StarSystem", new XAttribute("Name", system.SysName)));
            xDoc.Save(path);
        }
    }
}