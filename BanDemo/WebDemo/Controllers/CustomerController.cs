using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDemo.Models;
namespace WebDemo.Controllers
{
    public class CustomerController : Controller
    {

        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKi()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKi(Customer kh)
        {// chen du lieu vao bang
            if (ModelState.IsValid)
            {

                db.Customers.Add(kh);//chen vao bang
                db.SaveChanges();//luu vao csdl
            }
            
            return View();
        }
        //tao chuc nang dang nhap
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f )
        {
            string taiKhoan = f["txtTaiKhoan"].ToString();
            string matKhau = f.Get("txtMatKhau").ToString();

            Customer kh = db.Customers.SingleOrDefault(n => n.Id == taiKhoan && n.Password == matKhau);
            if (kh!=null) {
                ViewBag.ThongBao = "Chuc mung ban dang nhap thanh cong";
               // alert("Chuc mung ban da dang nhap thanh cong");
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index","Home");

            }
            ViewBag.ThongBao = "ban da dang nhap that bai";
            return View();
        }

    }
}