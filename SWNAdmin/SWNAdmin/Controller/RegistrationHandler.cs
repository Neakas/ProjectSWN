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
        public bool RegistrationCheck(Client c)
        {
            bool vUserExists = UserExists(c.UserName);

            if (vUserExists == false)
            {
                using (var context = new Utility.Db1Entities())
                {
                    Utility.Registration newReg = new Utility.Registration();
                    newReg.Username = c.UserName;
                    newReg.EMail = c.eMail;
                    newReg.Password = c.encPassword;
                    context.Registration.Add(newReg);
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
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
    }
}
