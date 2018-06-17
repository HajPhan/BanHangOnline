using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;

namespace WebDemo.Controllers
{
    public class HomeController : Controller
    {
        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index()
        {
            var model = db.Categories.Where(c => c.Products.Count > 4).ToList();//.Take(2).OrderBy(p=>Guid.NewGuid()).ToList()
            return View( model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Category()
        {
            var model = db.Categories;
            return PartialView("_Category", model);
        }
        public ActionResult Saleoff()
        {
            var model = db.Products.Where(p => p.Discount > 0).Take(2);//.Take(2).OrderBy(p=>Guid.NewGuid()).ToList()
            return PartialView("_Saleoff", model);
        }
        public ActionResult Special()
        {
            var model = db.Products.Where(p => p.Special==true);//.Take(2).OrderBy(p=>Guid.NewGuid()).ToList()
            return PartialView("_BestSeller", model);
        }
        public ActionResult Search(string Keywords = "")
        {
            var name = Request["term"];

            var data = db.Products
                .Where(p => p.Name.Contains(Keywords))
                .Select(p => p.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
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