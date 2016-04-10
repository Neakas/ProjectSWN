using System.IO;
using System.Reflection;
using System.Xml.Linq;
using SWNAdmin.Properties;
using SWNAdmin.Utility;

namespace SWNAdmin.Controller
{
    public static class SettingHandler
    {
        public static string GetIpPort()
        {
            var ipPort = XmlHandler.GrabXmlValue(GrabSettingFile(), "ipPort");
            return ipPort;
        }

        public static void SetIpPort( string ipPort )
        {
            XmlHandler.SetXmlValue(GrabSettingFile(), "ipPort", ipPort);
        }

        private static XDocument GrabSettingFile()
        {
            if (!CheckSettingFile())
            {
                return null;
            }
            var xDoc = XDocument.Load(SettingPath());
            return xDoc;
        }

        public static string SettingPath()
        {
            var targetPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (targetPath != null)
            {
                targetPath = targetPath.Remove(0, 6);
                const string settingFileName = "Settings.xml";
                var sPath = Path.Combine(targetPath, settingFileName);
                return sPath;
            }
            return null;
        }

        private static bool CheckSettingFile()
        {
            if (File.Exists(SettingPath()) == false)
            {
                SetSettingInit(false);
                File.Create(SettingPath()).Close();
            }
            if (GetSettingInit())
            {
                return true;
            }
            var doc = new XDocument(new XElement("body", new XElement("Db1ConnectionString"), new XElement("ipPort")));
            doc = XmlHandler.SetXmlValue(doc, "ipPort", "localhost:8000");
            doc.Save(SettingPath());
            SetSettingInit(true);
            return true;
        }

        private static bool GetSettingInit()
        {
            var settingInit = Settings.Default.SettingInit;
            return settingInit;
        }

        private static void SetSettingInit( bool boolean )
        {
            Settings.Default.SettingInit = boolean;
            Settings.Default.Save();
        }
    }
}