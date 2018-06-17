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
    public class ChiTietDonHangController : BaseController
    {
        // GET: Admin/ChiTietDonHang
        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var model = db.OrderDetails.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);

            return View(model);
        }


        //GET: Nhasx/Details
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail chitiet = db.OrderDetails.Find(id);
            if (chitiet == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }


        //GET: Nhasx/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: Nhasx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,ProductId,UnitPrice,Quantity,Discount")] Supplier chitiet)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(chitiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chitiet);
        }

        //GET: Nhasx/Edit
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail chitiet = db.OrderDetails.Find(id);
            if (chitiet == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }




        //POST: Nhasx/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,UnitPrice,Quantity,Discount")] Supplier chitiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chitiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chitiet);
        }



        //GET: Nhasx/Delete
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail chitiet = db.OrderDetails.Find(id);
            if (chitiet == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }

        //POST: Nhasx/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail chitiet = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(chitiet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}