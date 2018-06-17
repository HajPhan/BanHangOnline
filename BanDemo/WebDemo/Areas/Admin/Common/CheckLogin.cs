using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDemo.Areas.Admin.Models.Database;

namespace WebDemo.Areas.Admin.Common
{
    public class CheckLogin
    {
        DoDienTuEntities2 db = new DoDienTuEntities2();
        public int Login(string username, string password)
        {
            var result = db.Accounts.SingleOrDefault(x => x.Username == username);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.Password == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public Account GetById(string username)
        {
            return db.Accounts.SingleOrDefault(x => x.Username == username);
        }
    }
}