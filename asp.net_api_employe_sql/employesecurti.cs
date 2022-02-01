using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp.net_api_employe_sql
{
    public class employesecurti
    {

        public static bool login(string usrname,string password)
        {
            using (employedbEntities entities = new employedbEntities())
            {
                return entities.users.Any(user => user.username.Equals(usrname,
                    StringComparison.OrdinalIgnoreCase) && user.password == password);
            }
        }
    }
}