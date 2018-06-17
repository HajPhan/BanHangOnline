using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WebDemo.Areas.Admin.Controllers
{
    public class DonDatHangController : BaseController
    {
        // GET: Admin/DonDatHang

        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var model = db.Orders.ToList().ToPagedList(pageNumber, pageSize);
            return View(model);

        }

        //GET: Nhasx/Details
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order dondh = db.Orders.Find(id);
            if (dondh == null)
            {
                return HttpNotFound();
            }
            return View(dondh);
        }


        //GET: Nhasx/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: Nhasx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,OrderDate,RequireDate,Receiver,Address,Description,Amount")] Order dondh)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(dondh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dondh);
        }

        //GET: Nhasx/Edit
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order dondh = db.Orders.Find(id);
            if (dondh == null)
            {
                return HttpNotFound();
            }
            return View(dondh);
        }




        //POST: Nhasx/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,OrderDate,RequireDate,Receiver,Address,Description,Amount")] Order dondh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dondh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dondh);
        }



        //GET: Nhasx/Delete
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order dondh = db.Orders.Find(id);
            if (dondh == null)
            {
                return HttpNotFound();
            }
            return View(dondh);
        }

        //POST: Nhasx/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order dondh = db.Orders.Find(id);
            db.Orders.Remove(dondh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}