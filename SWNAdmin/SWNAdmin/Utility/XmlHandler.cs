using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using UniverseGeneration;
using SWNAdmin.Utility;

namespace SWNAdmin
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
            XElement xEle = new XElement(ElementName, Content);
            return xEle;
        }

        public static string GrabXMLValue(XDocument Document, string Element)
        {
            string Value = "";
            foreach (XElement xEle in Document.Element("body").Elements())
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
            foreach (XElement xEle in Document.Element("body").Elements())
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
            XDocument XDoc = new XDocument(new XElement("StarSystem", new XAttribute("Name", System.sysName)));
            XDoc.Save(Path);
        }

        public static void XML2SQL(skill_list SkillList)
        {
            //            var context = new Utility.Db1Entities();
            //var query = from c in context.StarSystems select c;
            //AllSystems = query.ToList();
        }
    }
}
