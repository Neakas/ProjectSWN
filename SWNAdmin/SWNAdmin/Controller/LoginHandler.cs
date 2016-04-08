using System;
using SWNAdmin.Classes;
using SWNAdmin.Utility;
using System.Linq;

namespace SWNAdmin.Controller
{
    public class LoginHandler
    {
        public static bool LoginCheck(Client cl)
        {
            using (var regcontext = new Db1Entities())
            {
                var query = from c in regcontext.Registration
                    where c.Username == cl.UserName && c.Password == cl.EncPassword
                    select c;
                try
                {
                    var firstOrDefault = query.FirstOrDefault();
                    var userId = firstOrDefault?.Id;

                    return userId > 0;
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return false;
        }
    }
}