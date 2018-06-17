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
    public class DanhMucSanPhamController : BaseController
    {
        // GET: Admin/dondhSanPham
        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var model = db.Categories.ToList().OrderBy(n => n.Id).ToPagedList(pageNumber, pageSize);

            return View(model);
        }


        //GET: Nhasx/Details
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category danmuc = db.Categories.Find(id);
            if (danmuc == null)
            {
                return HttpNotFound();
            }
            return View(danmuc);
        }



        //GET: Nhasx/Create
        public ActionResult Create()
        {
            return View();
        }


        //POST: Nhasx/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NameVN,Name,Icon")] Category danmuc)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(danmuc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danmuc);
        }

        //GET: Nhasx/Edit
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category danmuc = db.Categories.Find(id);
            if (danmuc == null)
            {
                return HttpNotFound();
            }
            return View(danmuc);
        }




        //POST: Nhasx/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameVN,Name,Icon")] Category danmuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danmuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danmuc);
        }



        //GET: Nhasx/Delete
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category danmuc = db.Categories.Find(id);
            if (danmuc == null)
            {
                return HttpNotFound();
            }
            return View(danmuc);
        }

        //POST: Nhasx/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category danmuc = db.Categories.Find(id);
            db.Categories.Remove(danmuc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}