using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Net.Http;
using BanHangOnline.Models;

namespace BanHangOnline.Areas.Admin.Controllers.mvc
{
    public class NhanVienController : BaseController
    {
        // GET: Admin/NhanVien
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            IEnumerable<NhanVienViewModel> nhanviens = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("nhanvien");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<NhanVienViewModel>>();
                    readTask.Wait();

                    nhanviens = readTask.Result;
                }
                else
                {
                    nhanviens = Enumerable.Empty<NhanVienViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contatct administractor.");
                }
            }

            return View(nhanviens.ToPagedList(pageNumber, pageSize));
        }


        // POST: Nhan Vien
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(NhanVienViewModel nhanvien)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/nhanvien");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<NhanVienViewModel>("nhanvien", nhanvien);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(nhanvien);
        }


        // PUT: Nhan Vien
        //public ActionResult Edit(int id)
        //{
        //    NhanVienViewModel nhanvien = null;

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("http://localhost:53017/api/");

        //        // HTTP GET
        //        var respontTask = client.GetAsync("nhanvien?id=" + id.ToString());
        //        respontTask.Wait();

        //        var result = respontTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<NhanVienViewModel>();
        //            readTask.Wait();

        //            nhanvien = readTask.Result;
        //        }
        //    }
        //    return View(nhanvien);
        //}



        public ActionResult Edit(int id)
        {
            NhanVienViewModel nhanvien = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var responseTask = client.GetAsync("nhanvien?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<NhanVienViewModel>();
                    readTask.Wait();

                    nhanvien = readTask.Result;
                }
            }

            return View(nhanvien);
        }



        [HttpPost]
        public ActionResult Edit(NhanVienViewModel nhanvien)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/nhanvien");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<NhanVienViewModel>("nhanvien", nhanvien);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(nhanvien);
        }

    }
}