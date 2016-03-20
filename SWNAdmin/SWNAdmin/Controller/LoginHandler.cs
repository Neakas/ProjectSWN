using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amexus.Encryption;
using System.Data.SqlClient;
using System.Data;
using System.ServiceModel;
using System.IO;

namespace SWNAdmin
{
    public class LoginHandler
    {
        public bool LoginCheck(Client cl)
        {
            using (var regcontext = new Utility.Db1Entities())
            {
                var query = from c in regcontext.Registration where c.Username == cl.UserName && c.Password == cl.encPassword select c;
                try
                {
                    var UserID = query.FirstOrDefault().Id;

                    if (UserID > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception) { }
            }
            return false;
        }
    }
}
