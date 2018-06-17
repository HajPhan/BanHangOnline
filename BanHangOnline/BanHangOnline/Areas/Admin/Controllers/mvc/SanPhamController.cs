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
using System.IO;

namespace BanHangOnline.Areas.Admin.Controllers.mvc
{
    public class SanPhamController : BaseController
    {
        BANHANGONLINEEntities5 db = new BANHANGONLINEEntities5();

        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            IEnumerable<SanPhamViewModel> sanphams = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var respontTask = client.GetAsync("sanpham");
                respontTask.Wait();

                var result = respontTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SanPhamViewModel>>();
                    readTask.Wait();

                    sanphams = readTask.Result;
                }
                else
                {
                    sanphams = Enumerable.Empty<SanPhamViewModel>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administractor.");
                }
            }

            return View(sanphams.ToPagedList(pageNumber, pageSize));
        }

        // POST: 
        public ActionResult create()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaSx = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TENSX), "MASX", "TENSX");
            ViewBag.MaLoai = new SelectList(db.LOAISPs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI"); ;
            return View();
        }

        [HttpPost]
        public ActionResult create(SanPhamViewModel sanpham, HttpPostedFileBase fileUpload)
        {

            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chon Anh";
                return View();
            }

            var fileName = Path.GetFileName(fileUpload.FileName);

            var path = Path.Combine(Server.MapPath("~/Images"), fileName);



            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "Images exists";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            sanpham.IMAGES = fileUpload.FileName;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/sanpham");

                // HTTP POST
                var postTask = client.PostAsJsonAsync<SanPhamViewModel>("sanpham", sanpham);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "sanpham");
                }

            }

            ModelState.AddModelError(string.Empty, "Server error. Please contact administractor");
            return View(sanpham);
        }


        public ActionResult Edit(int id)
        {
            SanPhamViewModel sanpham = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/");

                // HTTP GET
                var responseTask = client.GetAsync("sanpham?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = result.Content.ReadAsAsync<SanPhamViewModel>();
                    readTask.Wait();

                    sanpham = readTask.Result;
                }
            }

            return View(sanpham);
        }



        [HttpPost]
        public ActionResult Edit(SanPhamViewModel sanpham)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53017/api/nhanvien");

                //HTTP POST
                var putTask = client.PutAsJsonAsync<SanPhamViewModel>("sanpham", sanpham);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(sanpham);
        }

    }
}