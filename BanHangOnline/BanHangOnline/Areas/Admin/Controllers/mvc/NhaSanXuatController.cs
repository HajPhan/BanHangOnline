using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using PagedList;
using PagedList.Mvc;
using BanHangOnline.Models;

namespace BanHangOnline.Areas.Admin.Controllers.mvc
{
    public class NhaSanXuatController : BaseController
    {
        // GET: Admin/NhaCungCap
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            IEnumerable<NhaSanXuatViewModel> nhasxes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("NhaSanXuat");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<NhaSanXuatViewModel>>();
                    readTask.Wait();

                    nhasxes = readTask.Result;
                }
                else
                {
                    nhasxes = Enumerable.Empty<NhaSanXuatViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administractor.");
                }
            }

            return View(nhasxes.ToPagedList(pageNumber, pageSize));
        }


        // POST: Nhan Vien
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(NhaSanXuatViewModel nhasx)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/nhanvien");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<NhaSanXuatViewModel>("nhasanxuat", nhasx);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(nhasx);
        }

        //EDIT
        public ActionResult Edit(int id)
        {
            NhaSanXuatViewModel nhasx = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("nhasanxuat?id" + id.ToString());
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<NhaSanXuatViewModel>();
                    readTask.Wait();

                    nhasx = readTask.Result;
                }
            }

            return View(nhasx);
        }

    }
}