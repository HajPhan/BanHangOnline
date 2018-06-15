using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanHangOnline.Models.Data;

namespace BanHangOnline.Areas.Admin.Common
{
    public class CheckLogin
    {
        BANHANGONLINEEntities5 db = new BANHANGONLINEEntities5();
        public int Login(string username, string password)
        {
            var result = db.NHANVIENs.SingleOrDefault(x => x.USERNAMES == username);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if(result.PASSWORDS == password)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }

        public NHANVIEN GetById(string username)
        {
            return db.NHANVIENs.SingleOrDefault(x=>x.USERNAMES == username);
        }
    }
}