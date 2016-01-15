using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace SWNAdmin
{

    public class SettingHandler
    {
        public static bool GetIsLoggedIn()
        {
            bool isLoggedIN = SWNAdmin.Properties.Settings.Default.LoggedIn;
            return isLoggedIN;
        }

        public static void SetIsLoggedIn(bool Boolean)
        {
            SWNAdmin.Properties.Settings.Default.LoggedIn = Boolean;
            SWNAdmin.Properties.Settings.Default.Save();
        }

        public static string GetIPPort()
        {
            string IPPort = XmlHandler.GrabXMLValue(GrabSettingFile(), "IPPort");
            return IPPort;
        }

        public static void SetIPPort(string IPPort)
        {
            XmlHandler.SetXmlValue(GrabSettingFile(), "IPPort", IPPort);
        }

        public static XDocument GrabSettingFile()
        {
            if (CheckSettingFile(SettingPath()))
            {
                XDocument xDoc = XDocument.Load(SettingPath());
                return xDoc;
            }
            return null;
        }

        public static string SettingPath()
        {
            string TargetPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            TargetPath = TargetPath.Remove(0, 6);
            string SettingFileName = "Settings.xml";
            string sPath = Path.Combine(TargetPath, SettingFileName);
            return sPath;
        }

        private static bool CheckSettingFile(string Path)
        {
            if (File.Exists(SettingPath()) == false)
            {
                SetSettingInit(false);
                File.Create(SettingPath()).Close();
            }
            if (GetSettingInit() == false)
            {
                XDocument doc = new XDocument(new XElement("body", new XElement("Db1ConnectionString"), new XElement("IPPort")));
                doc = XmlHandler.SetXmlValue(doc, "IPPort", "localhost:8000");
                doc.Save(SettingPath());
                SetSettingInit(true);
            }
            return true;
        }

        public static bool GetSettingInit()
        {
            bool SettingInit = SWNAdmin.Properties.Settings.Default.SettingInit;
            return SettingInit;
        }

        public static void SetSettingInit(bool Boolean)
        {
            SWNAdmin.Properties.Settings.Default.SettingInit = Boolean;
            SWNAdmin.Properties.Settings.Default.Save();
        }
    }
}
