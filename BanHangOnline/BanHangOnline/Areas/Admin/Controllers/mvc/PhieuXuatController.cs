using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PagedList.Mvc;
using PagedList;
using BanHangOnline.Models;
using BanHangOnline.Models.Data;

namespace BanHangOnline.Areas.Admin.Controllers.mvc
{
    public class PhieuXuatController : BaseController
    {
        BANHANGONLINEEntities5 db = new BANHANGONLINEEntities5();

        // GET: Admin/PhieuXuat
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            IEnumerable<PhieuXuatViewModel> phieuxats = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("phieuxuat");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PhieuXuatViewModel>>();
                    readTask.Wait();

                    phieuxats = readTask.Result;
                }
                else
                {
                    phieuxats = Enumerable.Empty<PhieuXuatViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administractor.");
                }
            }

            return View(phieuxats.ToPagedList(pageNumber, pageSize));
        }


        // POST: 
        public ActionResult create()
        {
            ViewBag.MaKh = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.HOTEN), "MAKH", "HOTEN");
            ViewBag.MaSp = new SelectList(db.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
            ViewBag.MaNv = new SelectList(db.NHANVIENs.ToList().OrderBy(n => n.HOTEN), "MANV", "HOTEN");
            ViewBag.MaDh = new SelectList(db.DONDHs.ToList().OrderBy(n => n.MADH), "MADH", "MADH");

            return View();
        }

        [HttpPost]
        public ActionResult create(PhieuXuatViewModel phieuxuat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/phieuxuat");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<PhieuXuatViewModel>("phieuxuat", phieuxuat);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(phieuxuat);
        }


        public ActionResult Edit(int id)
        {
            PhieuXuatViewModel phieuxuat = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var responseTask = client.GetAsync("phieuxuat?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<PhieuXuatViewModel>();
                    readTask.Wait();

                    phieuxuat = readTask.Result;
                }
            }

            return View(phieuxuat);
        }



        [HttpPost]
        public ActionResult Edit(PhieuXuatViewModel phieuxuat)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/nhanvien");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<PhieuXuatViewModel>("phieuxuat", phieuxuat);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(phieuxuat);
        }

    }
}