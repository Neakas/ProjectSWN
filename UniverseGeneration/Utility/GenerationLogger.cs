using System;
using System.IO;

namespace UniverseGeneration.Utility
{
    public class GenerationLogger
    {
        public string TargetPath { get; set; }
        public int LogCounter { get; set; }
        
        public void Initialize()
        {
            TargetPath = @"C:\Test\GenLog.txt";

            if (File.Exists(TargetPath))
            {
                File.Delete(TargetPath);
            }

            LogCounter = 0;

            using (StreamWriter writer = new StreamWriter(TargetPath,true))
            {
                writer.WriteLine("-----------------SWN Generation Log File-----------------");
                writer.WriteLine("-----------------------Initialized-----------------------");
            }
            
        }
        public void Initialize(string targetpath)
        {
            TargetPath = targetpath + "GenLog.txt";

            if (File.Exists(TargetPath))
            {
                File.Delete(TargetPath);
            }

            LogCounter = 0;

            using (StreamWriter writer = new StreamWriter(TargetPath, true))
            {
                writer.WriteLine("-----------------SWN Generation Log File-----------------");
                writer.WriteLine("-----------------------Initialized-----------------------");
            }
        }

        public void LogString(string ToLogString)
        {
            if (TargetPath == null)
            {
                Initialize();
                Exception e = new Exception("Please Initialize() the Logger First next Time. Basic Path Set!");
                return;
            }
            LogCounter++;
            using (StreamWriter writer = new StreamWriter(TargetPath, true))
            {
                writer.WriteLine(LogCounter + ":[" + ToLogString + "]");
            }
        }
        public void LogPriorityString(string ToLogString)
        {
            if (TargetPath == null)
            {
                Initialize();
                Exception e = new Exception("Please Initialize() the Logger First next Time. Basic Path Set!");
                return;
            }
            using (StreamWriter writer = new StreamWriter(TargetPath, true))
            {
                writer.WriteLine("---------------![" + ToLogString + "]!---------------");
            }
        }
    }
}
