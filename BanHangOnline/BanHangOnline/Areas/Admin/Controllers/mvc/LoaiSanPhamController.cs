using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Net.Http;
using BanHangOnline.Models;

namespace BanHangOnline.Areas.Admin.Controllers.mvc
{
    public class LoaiSanPhamController : BaseController
    {
        // GET: Admin/LoaiSanPham
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            IEnumerable<LoaiSanPhamViewModel> loaisp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("loaisanpham");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<LoaiSanPhamViewModel>>();
                    readTask.Wait();

                    loaisp = readTask.Result;
                }
                else
                {
                    loaisp = Enumerable.Empty<LoaiSanPhamViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administractor.");
                }
            }
            return View(loaisp.ToPagedList(pageNumber, pageSize));
        }


        // POST: 
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(LoaiSanPhamViewModel loaisp)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/loaisanpham");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<LoaiSanPhamViewModel>("loaisanpham", loaisp);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(loaisp);
        }
    }
}