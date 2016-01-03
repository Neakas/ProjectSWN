using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SWNAdmin
{


    class UndoHandler
    {
        public static int MaximumUndo = 5;
        public static List<DateTime> UndoList = new List<DateTime>();
        public static DateTime[] TempUndoList = new DateTime[MaximumUndo];
        //erstellt eine ObservvableColletion. Im Grunde ein Array, was aber nicht an GrößenDefinitionen gebunden ist.
        public static ObservableCollection<DateTime> DateTimeCollection = new ObservableCollection<DateTime>();

        //Fügt DateTimeItems der Collection Hinzu
        public static void AddItemToUndoList(DateTime DateTimeItem)
        {
            if (DateTimeCollection.Count <= MaximumUndo)
            {
                if (DateTimeCollection.Count == MaximumUndo)
                {
                    DateTimeCollection.RemoveAt(0);
                }
                DateTimeCollection.Add(DateTimeItem);
                SettingHandler.SethasUndo(true);
            }
        }

        //Gibt die Aktuelle Collection zurück
        public static ObservableCollection<DateTime> ReturnCollection()
        {
            return DateTimeCollection;
        }

        //Gibt das Letzte Undo DateTime zurück
        public static DateTime getUndo()
        {
            DateTime UndoDateTime = SettingHandler.GetCurrentDateTime();
            if (DateTimeCollection.Count > 0)
            {
                UndoDateTime = DateTimeCollection.Last();
                DateTimeCollection.RemoveAt(DateTimeCollection.Count - 1);
                return UndoDateTime;
            }
            return UndoDateTime;
        }
    }
}
