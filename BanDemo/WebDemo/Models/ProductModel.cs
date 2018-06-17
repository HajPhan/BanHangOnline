using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace WebDemo.Models
{
    public class ProductModel
    {
        private MultiShopDbContext connext = null;
        public ProductModel()
        {
            connext = new MultiShopDbContext();
        }
        public List<Product> ListAll()
        {
            var list = connext.Database.SqlQuery<Product>("Sp_Category").ToList();
            return list;
        }
        public int Create(int Id, string Name, double UnitPrice, string Description,int Quantity )
        {
            Object[] paramester = 
           {
             new SqlParameter("Id",Id),
             new SqlParameter("Name",Name),
           //  new SqlParameter("UnitBrief",UnitBrief),
             new SqlParameter("UnitPrice",UnitPrice),
           //  new SqlParameter("Image",Image),
           //  new SqlParameter("ProductDate",ProductDate),
           ///  new SqlParameter("Available",Available),
             new SqlParameter("Description",Description),
           //  new SqlParameter("CategoryId",CategoryId),
           //  new SqlParameter("SupplierId",SupplierId),
             new SqlParameter("Quantity",Quantity),
            // new SqlParameter("Discount",Discount),
            // new SqlParameter("Special",Special),
            // new SqlParameter("Latest",Latest),
             // new SqlParameter("Views",Views)
//

           };
            int res = connext.Database.ExecuteSqlCommand("insert_Product @id,@name,@UnitPrice,@Discription,@Quantity", paramester);
            return res;
        }

    }

}



