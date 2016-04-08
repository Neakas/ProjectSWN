using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using SWN.Properties;

namespace SWN.Controller
{
    public class SettingHandler
    {
        public static List<string> ImageList = new List<string>();

        public static DateTime GetCurrentDateTime()
        {
            return Settings.Default.CurrentDateTime;
        }

        //Setzt die Aktuelle DateTime
        public static void SetCurrentDateTime(DateTime dateTimeValue, bool isUndo = false)
        {
            Settings.Default.CurrentDateTime = dateTimeValue;
            Settings.Default.Save();
        }

        public static bool GetisStartDate()
        {
            var isStartDate = Settings.Default.isStartDate;
            return isStartDate;
        }

        public static void SetisStartDate(bool boolean)
        {
            Settings.Default.isStartDate = boolean;
            Settings.Default.Save();
        }

        public static bool GethasUndo()
        {
            return Settings.Default.hasUndo;
        }

        public static void SethasUndo(bool boolean)
        {
            Settings.Default.hasUndo = boolean;
            Settings.Default.Save();
        }

        public static bool GetIsLoggedIn()
        {
            return Settings.Default.LoggedIn;
        }

        public static void SetIsLoggedIn(bool boolean)
        {
            Settings.Default.LoggedIn = boolean;
            Settings.Default.Save();
        }

        public static string GetIpPort()
        {
            return XmlHandler.GrabXmlValue(GrabSettingFile(), "ipPort");
        }

        public static void SetIpPort(string ipPort)
        {
            XmlHandler.SetXmlValue(GrabSettingFile(), "ipPort", ipPort);
        }

        public static bool GetTurnOffMusic()
        {
            return bool.Parse(XmlHandler.GrabXmlValue(GrabSettingFile(), "TurnOffMusic"));
        }

        public static void TurnOffMusic(bool value)
        {
            value = !value;
            XmlHandler.SetXmlValue(GrabSettingFile(), "TurnOffMusic", value.ToString());
        }

        public static XDocument GrabSettingFile()
        {
            if (!File.Exists(SettingPath())) return null;
            var xDoc = XDocument.Load(SettingPath());
            return xDoc;
        }

        public static string SettingPath()
        {
            var targetPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (targetPath == null) return null;
            targetPath = targetPath.Remove(0, 6);
            const string settingFileName = "Config.cfg";
            var sPath = Path.Combine(targetPath, settingFileName);
            return sPath;
        }

        public static void InitSettingFile()
        {
            if (XmlHandler.GrabXmlValue(GrabSettingFile(), "FirstLoad") != "true") return;
            var targetPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (targetPath != null)
            {
                targetPath = targetPath.Remove(0, 6);
                var picDir = Path.Combine(targetPath, "Received Images");
                var fileDir = Path.Combine(targetPath, "Received Files");
                Directory.CreateDirectory(picDir);
                XmlHandler.SetXmlValue(GrabSettingFile(), "FirstLoad", "false");
                XmlHandler.SetXmlValue(GrabSettingFile(), "PicFilePath", picDir);
                XmlHandler.SetXmlValue(GrabSettingFile(), "DataFilePath", fileDir);
            }
            XmlHandler.SetXmlValue(GrabSettingFile(), "ipPort", GetIpPort());
        }

        public static void PreloadImages()
        {
            foreach (var s in Directory.GetFiles(XmlHandler.GrabXmlValue(GrabSettingFile(), "PicFilePath"), "*.*").Select(Path.GetFileName))
            {
                ImageList.Add(s);
            }
        }
    }
}