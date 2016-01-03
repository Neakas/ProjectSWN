using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using amexus.Encryption;


namespace SWNAdmin
{
    public class RegistrationHandler
    {
        public int RegistrationCheck(string UserName, string eMail, string encPassword)
        {
            bool IsAdmin = false;
            bool vUserExists = UserExists(UserName);
            if (CheckIsFirstUser())
            {
                IsAdmin = true;
            }

            if (vUserExists == false)
            {
                using (var context = new Utility.Db1Entities())
                {
                    Utility.Registration newReg = new Utility.Registration();
                    newReg.Username = UserName;
                    newReg.EMail = eMail;
                    newReg.Password = encPassword;
                    newReg.isAdmin = IsAdmin;
                    context.Registration.Add(newReg);
                    context.SaveChanges();
                }
                if (IsAdmin == true)
                    return 4;
                else
                    return 1;
            }
            else if (vUserExists == true)
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

        private bool UserExists(String UserName)
        {
            //grundsätzlich existiert er nicht
            bool UserExists = false;
            //Neue SQL verbindung

            var context = new Utility.Db1Entities();
            var query = from c in context.Registration where c.Username == UserName select c;
            var adv = query.FirstOrDefault();

            if (adv != null)
            {
                //User existiert
                UserExists = true;
            }
            return UserExists;
        }
        
        //Überprüft ob der Benutzer der erste Benutzer ist, der sich an der Datenbank anmelden. Wenn das der Fall ist wird ihm automatisch Admin-Rechte gegeben
        private bool CheckIsFirstUser()
        {
            var context = new Utility.Db1Entities();
            var query = from c in context.Registration select c;
            var Count = query.Count();
           
            if (Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
