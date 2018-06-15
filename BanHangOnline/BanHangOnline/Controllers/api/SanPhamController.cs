using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BanHangOnline.Models;
using BanHangOnline.Models.Data;

namespace BanHangOnline.Controllers
{
    public class SanPhamController : ApiController
    {

        public IHttpActionResult GetAllSanPham()
        {
            IList<SanPhamViewModel> sanpham = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                sanpham = ctx.SANPHAMs.Select(s => new SanPhamViewModel()
                {
                    MASP = s.MASP,
                    TENSP = s.TENSP,
                    CHITIET = s.CHITIET,
                    IMAGES = s.IMAGES,
                    TRANGTHAI = s.TRANGTHAI,
                    GIA = s.GIA,
                    GIAMGIA = s.GIAMGIA,
                    SL = s.SL,
                    MASX = s.MASX,
                    MALOAI = s.MALOAI
                }).ToList<SanPhamViewModel>();
            }

            if(sanpham.Count == 0)
            {
                return NotFound();
            }

            return Ok(sanpham);
        }

        //
        public IHttpActionResult PostNewSanPham(SanPhamViewModel sanpham)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemSanPham(sanpham.TENSP, sanpham.GIA, sanpham.CHITIET, sanpham.IMAGES, sanpham.TRANGTHAI, sanpham.GIAMGIA, sanpham.SL, sanpham.MASX, sanpham.MALOAI);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}
