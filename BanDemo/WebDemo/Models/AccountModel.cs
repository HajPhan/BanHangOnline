using WebDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Models
{
    public class AccountModel
    {

        private MultiShopDbContext connext = null;
        public AccountModel()
        {
            connext = new MultiShopDbContext();
        }
        public bool Login(string username, string password)
        {
           Object[] sqlPama= 
            {
                new SqlParameter("@Username",username),

            new SqlParameter("@Password", password)
        };
            var res = connext.Database.SqlQuery<bool>("Account_Login @Username,@Password",sqlPama).SingleOrDefault();
            return res;
        }
    }
}
