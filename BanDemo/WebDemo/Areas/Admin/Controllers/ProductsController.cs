using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using WebDemo.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace WebDemo.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Admin/Category
        public ActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            var ipCategory = new ProductModel();
            // var model = ipCategory.ListAll();
            var model = ipCategory.ListAll().ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);
            return View(model);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpGet]

        public ActionResult Edit(int Id)
        {
            ViewBag.CategoryID = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.Suppliers = new SelectList(db.Suppliers.ToList(), "Id", "Name");
            Product product = db.Products.Single(n => n.Id == Id);
            return View(product);
        }
        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Edit(Product Ma, int CategoryID, string Suppliers)
        {
            //  db.Products.(Id);
            if (ModelState.IsValid)
            {
                Ma.SupplierId = (Suppliers);

                Ma.CategoryId = CategoryID;
                // Ma.UnitBrief ="";
                Ma.ProductDate = DateTime.Now;
                //  Ma.SupplierId = "";
                db.Entry(Ma).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product collection)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var model = new ProductModel();
                    int res = model.Create(collection.Id, collection.Name, collection.UnitPrice, collection.Description, collection.Quantity);
                    if (res > 0)
                    {

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Them moi khong thanh cong");
                    }

                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Xoa(int Id)
        {
            Product product = db.Products.Single(n => n.Id == Id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Them()
        {
            ViewBag.categoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Them(Product pro, HttpPostedFileBase file, int categoryID, string supplierId)
        {
            pro.CategoryId = categoryID;
            pro.SupplierId = supplierId;
            if (file == null)
            {
                ViewBag.categoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
                ViewBag.SupplierId = new SelectList(db.Suppliers.ToList(), "Id", "Name");
                ViewBag.Erro = "xin moi chon anh";

                return View();

            }
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);

               // F:\Kì 2 năm 3\Công nghệ Web\BanDemo\WebDemo\Content\img\products\1017.png
                var path = Path.Combine(Server.MapPath("~/Content/img/products"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.categoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
                    ViewBag.SupplierId = new SelectList(db.Suppliers.ToList(), "Id", "Name");
                    ViewBag.Erro = "Hinh anh da ton tai";
                    return View();

                }
                else
                {
                    file.SaveAs(path);

                }
                pro.Image = file.FileName;
                db.Products.Add(pro);
                db.SaveChanges();
            }


            return RedirectToAction("Index","Products");
        }


    }
}