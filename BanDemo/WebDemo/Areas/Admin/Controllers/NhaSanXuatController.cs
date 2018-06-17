using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WebDemo.Areas.Admin.Controllers
{
    public class NhaSanXuatController : BaseController
    {
        // GET: 
        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var model = db.Suppliers.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        //GET: 
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier nhasx = db.Suppliers.Find(id);
            if (nhasx == null)
            {
                return HttpNotFound();
            }
            return View(nhasx);
        }


        //GET: 
        public ActionResult Create()
        {
            return View();
        }


        //POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Logo,Email,Phone")] Supplier nhasx)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(nhasx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhasx);
        }

        //GET: 
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier nhasx = db.Suppliers.Find(id);
            if (nhasx == null)
            {
                return HttpNotFound();
            }
            return View(nhasx);
        }




        //POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Logo,Email,Phone")] Supplier nhasx)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhasx).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhasx);
        }



        //GET:
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier nhasx = db.Suppliers.Find(id);
            if (nhasx == null)
            {
                return HttpNotFound();
            }
            return View(nhasx);
        }

        //POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Supplier nhasx = db.Suppliers.Find(id);
            db.Suppliers.Remove(nhasx);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}