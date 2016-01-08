using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

namespace SWN
{
    
    public class SettingHandler
    {
        public static List<string> ImageList = new List<string>();

        public static DateTime GetCurrentDateTime()
        {
            DateTime CurrentDateTime = SWN.Properties.Settings.Default.CurrentDateTime;
            return CurrentDateTime;
        }

        //Setzt die Aktuelle DateTime
        public static void SetCurrentDateTime(DateTime DateTimeValue, bool isUndo = false)
        {
            //cleanup erstmal rausgenommen
            //if (isUndo == false)
            //{
            //    UndoHandler.AddItemToUndoList(GetCurrentDateTime());
            //}
                SWN.Properties.Settings.Default.CurrentDateTime = DateTimeValue;
                SWN.Properties.Settings.Default.Save();
        }

        public static bool GetisStartDate()
        {
            bool isStartDate = SWN.Properties.Settings.Default.isStartDate;
            return isStartDate;
        }

        public static void SetisStartDate(bool Boolean)
        {
            SWN.Properties.Settings.Default.isStartDate = Boolean;
            SWN.Properties.Settings.Default.Save();
        }
        
        public static bool GethasUndo()
        {
            bool hasUndo = SWN.Properties.Settings.Default.hasUndo;
            return hasUndo;
        }

        public static void SethasUndo(bool Boolean)
        {
            SWN.Properties.Settings.Default.hasUndo = Boolean;
            SWN.Properties.Settings.Default.Save();
        }

        public static bool GetIsLoggedIn()
        {
            bool isLoggedIN = SWN.Properties.Settings.Default.LoggedIn;
            return isLoggedIN;
        }

        public static void SetIsLoggedIn(bool Boolean)
        {
            SWN.Properties.Settings.Default.LoggedIn = Boolean;
            SWN.Properties.Settings.Default.Save();
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
            if (File.Exists(SettingPath()))
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
            string SettingFileName = "Config.cfg";
            string sPath = Path.Combine(TargetPath, SettingFileName);
            return sPath;
        }

        public static void InitSettingFile()
        {
            if (XmlHandler.GrabXMLValue(SettingHandler.GrabSettingFile(), "FirstLoad") == "true")
            {
                string picTargetFolderName = "Received Images";
                string fileTargetFolderName = "Received Files";
                string TargetPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                TargetPath = TargetPath.Remove(0, 6);
                string picDir = Path.Combine(TargetPath, picTargetFolderName);
                string fileDir = Path.Combine(TargetPath, fileTargetFolderName);
                Directory.CreateDirectory(picDir);
                XmlHandler.SetXmlValue(SettingHandler.GrabSettingFile(), "FirstLoad", "false");
                XmlHandler.SetXmlValue(SettingHandler.GrabSettingFile(), "PicFilePath", picDir);
                XmlHandler.SetXmlValue(SettingHandler.GrabSettingFile(), "DataFilePath", fileDir);
                XmlHandler.SetXmlValue(SettingHandler.GrabSettingFile(), "IPPort", "localhost:8001");
            }
        }

        //Cleanup Delete if not needed
        //private static bool CheckSettingFile(string Path)
        //{
        //    if (File.Exists(SettingPath()) == false)
        //    {
        //        SetSettingInit(false);
        //        File.Create(SettingPath()).Close();
        //    }
        //    if (GetSettingInit() == false)
        //    {
        //        XDocument doc = new XDocument(new XElement("body", new XElement("Db1ConnectionString"), new XElement("IPPort")));
        //        doc = XmlHandler.SetXmlValue(doc, "IPPort", "localhost:8000");
        //        doc.Save(SettingPath());
        //        SetSettingInit(true);
        //    }
        //    return true;
        //}


        public static void SetUserName(string UserName)
        {
            SWN.Properties.Settings.Default.UserName = UserName;
            SWN.Properties.Settings.Default.Save();
        }

        public static string GetUserName()
        {
            string UserName = SWN.Properties.Settings.Default.UserName;
            return UserName;
        }

        public static void PreloadImages()
        {
            foreach (string s in Directory.GetFiles(XmlHandler.GrabXMLValue(SettingHandler.GrabSettingFile(), "PicFilePath"), "*.*").Select(Path.GetFileName))
            {
                ImageList.Add(s);
            }
        }

    }
}
