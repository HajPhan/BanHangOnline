using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;
using WebDemo.Areas.Admin.Models;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity;

namespace WebDemo.Areas.Admin.Controllers
{
    public class KhachHangController : BaseController
    {
        // GET: Admin/KhachHang
        MultiShopDbContext db = new MultiShopDbContext();
        
        public ActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);

            // var model = ipCategory.ListAll();
            var model = db.Customers.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);
            return View(model);
        }

        //GET: Nhasx/Details
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer khachhang = db.Customers.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }


        //GET: Nhasx/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: Nhasx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Password,Fullname,Email,Photo,Activated")] Customer khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(khachhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }

        //GET: Nhasx/Edit
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer khachhang = db.Customers.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }




        //POST: Nhasx/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Password,Fullname,Email,Photo,Activated")] Customer khachhang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }



        //GET: Nhasx/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer khachhang = db.Customers.Find(id);
            if (khachhang == null)
            {
                return HttpNotFound();
            }
            return View(khachhang);
        }

        //POST: Nhasx/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer khachhang = db.Customers.Find(id);
            db.Customers.Remove(khachhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}