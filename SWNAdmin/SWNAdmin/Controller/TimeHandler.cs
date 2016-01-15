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
            using (var context = new Utility.Db1Entities())
            {
                Utility.UDateTime UDateTime = new Utility.UDateTime();
                DateTime dt = new DateTime(3000, 1, 1);
                UDateTime.CurrentDateTime = dt;
                UDateTime.SystemTime = DateTime.Now;
                context.UDateTime.Add(UDateTime);
                context.SaveChanges();
            }
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Tagen und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementDay(int Amount)
        {
            DateTime CurrentDateTime = GetCurrentDateTime();
            CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromDays(Amount));
            SetCurrentDateTime(CurrentDateTime);
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Minuten und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementMinute(int Amount)
        {
            DateTime CurrentDateTime = GetCurrentDateTime();
            CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromMinutes(Amount));
            SetCurrentDateTime(CurrentDateTime);
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Stunden und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementHour(int Amount)
        {
            DateTime CurrentDateTime = GetCurrentDateTime();
            CurrentDateTime = CurrentDateTime.Add(TimeSpan.FromHours(Amount));
            SetCurrentDateTime(CurrentDateTime);
        }

        public static DateTime GetCurrentDateTime()
        {
            var timecontext = new Utility.Db1Entities();
            var query = from c in timecontext.UDateTime orderby c.Id descending select c;
            var filterquery = query.FirstOrDefault();
            return (DateTime)filterquery.CurrentDateTime;
        }

        public static void SetCurrentDateTime(DateTime Dt)
        {
            using (var context = new Utility.Db1Entities())
            {
                Utility.UDateTime UDateTime = new Utility.UDateTime();
                UDateTime.CurrentDateTime = Dt;
                UDateTime.SystemTime = DateTime.Now;
                context.UDateTime.Add(UDateTime);
                context.SaveChanges();
            }
        }
    }
}
