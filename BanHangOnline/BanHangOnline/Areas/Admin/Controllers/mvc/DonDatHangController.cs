using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net.Http;
using BanHangOnline.Models;
using BanHangOnline.Models.Data;

namespace BanHangOnline.Areas.Admin.Controllers.mvc
{
    public class DonDatHangController : BaseController
    {
        BANHANGONLINEEntities5 db = new BANHANGONLINEEntities5();

        // GET: Admin/DonDatHang
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            IEnumerable<DonDatHangViewModel> dondhs = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("dondathang");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DonDatHangViewModel>>();
                    readTask.Wait();

                    dondhs = readTask.Result;
                }
                else
                {
                    dondhs = Enumerable.Empty<DonDatHangViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact adminstractor.");
                }
            }

            return View(dondhs.ToPagedList(pageNumber, pageSize));
        }

        // POST: 
        public ActionResult create()
        {
            ViewBag.MaKh = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HOTEN), "MAKH", "HOTEN");
            ViewBag.MaSp = new SelectList(db.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP"); 
            ViewBag.MaNv = new SelectList(db.NHANVIENs.ToList().OrderBy(n => n.HOTEN), "MANV", "HOTEN");
            return View();
        }

        [HttpPost]
        public ActionResult create(DonDatHangViewModel dondh)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/dondathang");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<DonDatHangViewModel>("dondathang", dondh);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(dondh);
        }


        public ActionResult Edit(int id)
        {
            DonDatHangViewModel dondh = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var responseTask = client.GetAsync("dondathang?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<DonDatHangViewModel>();
                    readTask.Wait();

                    dondh = readTask.Result;
                }
            }

            return View(dondh);
        }



        [HttpPost]
        public ActionResult Edit(DonDatHangViewModel dondh)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/nhanvien");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<DonDatHangViewModel>("dondathang", dondh);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(dondh);
        }

    }
}