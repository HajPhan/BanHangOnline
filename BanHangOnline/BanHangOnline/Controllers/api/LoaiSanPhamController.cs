using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BanHangOnline.Models;
using BanHangOnline.Models.Data;

namespace BanHangOnline.Controllers
{
    public class LoaiSanPhamController : ApiController
    {
        public IHttpActionResult GetAllLoaiSanPham()
        {
            IList<LoaiSanPhamViewModel> loaisp = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                loaisp = ctx.LOAISPs.Select(s => new LoaiSanPhamViewModel()
                {
                    MALOAI = s.MALOAI,
                    TENLOAI = s.TENLOAI,
                    DVT = s.DVT
                }).ToList<LoaiSanPhamViewModel>();
            }

            if(loaisp.Count == 0)
            {
                return NotFound();
            }

            return Ok(loaisp);
        }

        public IHttpActionResult PostNewLoaiSanPham(LoaiSanPhamViewModel loaisp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemLoaiSanPham(loaisp.TENLOAI, loaisp.DVT);
                ctx.SaveChanges();
            }

            return Ok();
        }
    }

}
