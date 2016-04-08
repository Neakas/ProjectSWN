using SWNAdmin.Classes;
using SWNAdmin.Utility;
using System.Linq;

namespace SWNAdmin.Controller
{
    public class RegistrationHandler
    {
        public bool RegistrationCheck(Client c)
        {
            var vUserExists = UserExists(c.UserName);

            if (vUserExists) return false;
            using (var context = new Db1Entities())
            {
                var newReg = new Registration
                {
                    Username = c.UserName,
                    EMail = c.EMail,
                    Password = c.EncPassword
                };
                context.Registration.Add(newReg);
                context.SaveChanges();
            }
            return true;
        }

        private static bool UserExists(string userName)
        {
            //grundsätzlich existiert er nicht
            var userExists = false;
            //Neue SQL verbindung

            var context = new Db1Entities();
            var query = from c in context.Registration where c.Username == userName select c;
            var adv = query.FirstOrDefault();

            if (adv != null)
            {
                //User existiert
                userExists = true;
            }
            return userExists;
        }
    }
}