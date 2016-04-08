using System;
using System.Linq;
using SWNAdmin.Utility;

namespace SWNAdmin.Controller
{
    public static class TimeHandler
    {
        //Erhöht die CurrentDateTime um die angegebene Menge an Tagen und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementDay(int amount)
        {
            var currentDateTime = GetCurrentDateTime();
            currentDateTime = currentDateTime.Add(TimeSpan.FromDays(amount));
            SetCurrentDateTime(currentDateTime);
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Minuten und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementMinute(int amount)
        {
            var currentDateTime = GetCurrentDateTime();
            currentDateTime = currentDateTime.Add(TimeSpan.FromMinutes(amount));
            SetCurrentDateTime(currentDateTime);
        }

        //Erhöht die CurrentDateTime um die angegebene Menge an Stunden und schreibt den Neuen Wert zurück in die GlobalSettings
        public static void IncrementHour(int amount)
        {
            var currentDateTime = GetCurrentDateTime();
            currentDateTime = currentDateTime.Add(TimeSpan.FromHours(amount));
            SetCurrentDateTime(currentDateTime);
        }

        public static DateTime GetCurrentDateTime()
        {
            var timecontext = new Db1Entities();
            var query = from c in timecontext.UDateTime orderby c.Id descending select c;
            var filterquery = query.FirstOrDefault();
            return filterquery?.CurrentDateTime ?? new DateTime();
        }

        public static void SetCurrentDateTime(DateTime dt)
        {
            using (var context = new Db1Entities())
            {
                var uDateTime = new UDateTime
                {
                    CurrentDateTime = dt,
                    SystemTime = DateTime.Now
                };
                context.UDateTime.Add(uDateTime);
                context.SaveChanges();
            }
        }
    }
}