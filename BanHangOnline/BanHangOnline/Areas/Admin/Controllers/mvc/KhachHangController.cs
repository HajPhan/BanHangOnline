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
    public class KhachHangController : BaseController
    {
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            IEnumerable<KhachHangViewModel> khachhangs = null;
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("khachhang");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<KhachHangViewModel>>();

                    khachhangs = readTask.Result;
                }
                else
                {
                    khachhangs = Enumerable.Empty<KhachHangViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administractor.");
                }
            }


            return View(khachhangs.ToPagedList(pageNumber, pageSize));
        }


        // POST: 
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(KhachHangViewModel khachhang)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/khachhang");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<KhachHangViewModel>("khachhang", khachhang);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(khachhang);
        }


        public ActionResult Edit(int id)
        {
            KhachHangViewModel khachhang = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var responseTask = client.GetAsync("khachhang?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<KhachHangViewModel>();
                    readTask.Wait();

                    khachhang = readTask.Result;
                }
            }

            return View(khachhang);
        }



        [HttpPost]
        public ActionResult Edit(KhachHangViewModel khachhang)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/khachhang");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<KhachHangViewModel>("khachhang", khachhang);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(khachhang);
        }

    }
}