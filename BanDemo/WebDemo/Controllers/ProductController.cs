using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;
using PagedList.Mvc;
using PagedList;
namespace WebDemo.Controllers
{
    public class ProductController : Controller
    {
        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(int Category_ID, int? page )
        {
           
            //  Category_ID = 0;
            if (Category_ID!= 0)
            {
                
                int pageSize = 4;
                int pageNumber = (page ?? 1);
                var ipCategory = new ProductModel();
                // var model = db.Products.Where(p => p.CategoryId==Category_ID).OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);
                //  var model1 = db.Products.Where(p => p.CategoryId == Category_ID).ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);
                var model = ipCategory.ListAll().ToList().Where(p => p.CategoryId == Category_ID).OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize);
                return View(model);

            }
            return View();




        }
        public ActionResult Detail(int id)
        {
            var model = db.Products.Find(id);
            return View(model);
        }
        public ActionResult Search(string Keywords = "")
        {
            if (Keywords != "")
            {
                var model = db.Products
                    .Where(p => p.Name.Contains(Keywords));
                return View(model);

            }
            return View(db.Products);
        }
    }
}