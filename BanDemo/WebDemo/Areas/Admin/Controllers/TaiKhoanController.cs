using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Areas.Admin.Models.Database;
using PagedList;
using PagedList.Mvc;
using System.Net;
using System.Data.Entity;

namespace WebDemo.Areas.Admin.Controllers
{
    public class TaiKhoanController : BaseController
    {
        // GET: Admin/TaiKhoan
        DoDienTuEntities2 db = new DoDienTuEntities2();
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var model = db.Accounts.ToList().ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        //GET: 
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account taikhoan = db.Accounts.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }


        //GET:
        public ActionResult Create()
        {
            return View();
        }


        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password")] Account taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(taikhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taikhoan);
        }

        //GET:
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account taikhoan = db.Accounts.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }


        //POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password")] Account taikhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taikhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taikhoan);
        }



        //GET: 
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account taikhoan = db.Accounts.Find(id);
            if (taikhoan == null)
            {
                return HttpNotFound();
            }
            return View(taikhoan);
        }

        //POST: 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account taikhoan = db.Accounts.Find(id);
            db.Accounts.Remove(taikhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}