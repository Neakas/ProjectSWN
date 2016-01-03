using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SWNAdmin
{
    public class TimeHandler
    {

        //setzt das StartDatum
        public static void ResetDateTime()
        {
            //TestVar zum resetten.Später rausnehmen!
            SettingHandler.SetisStartDate(true);

            if (SettingHandler.GetisStartDate())
            {
                DateTime StartDate = new DateTime(3000, 1, 1, 0, 0, 0);
                SettingHandler.SetCurrentDateTime(StartDate);
                SettingHandler.SetisStartDate(false);
            }
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Tagen und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementDay(int Amount)
        {
            DateTime CurrentDateTime = SettingHandler.GetCurrentDateTime();
            CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromDays(Amount));
            SettingHandler.SetCurrentDateTime(CurrentDateTime);
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Minuten und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementMinute(int Amount)
        {
            DateTime CurrentDateTime = SettingHandler.GetCurrentDateTime();
            CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromMinutes(Amount));
            SettingHandler.SetCurrentDateTime(CurrentDateTime);
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Stunden und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementHour(int Amount)
        {
            DateTime CurrentDateTime = SettingHandler.GetCurrentDateTime();
            CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromHours(Amount));
            SettingHandler.SetCurrentDateTime(CurrentDateTime);
        }
    }
}
