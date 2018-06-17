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
    public class KhachHangController : ApiController
    {
        public IHttpActionResult GetAllKhachHang()
        {
            IList<KhachHangViewModel> khachhang = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                khachhang = ctx.KHACHHANGs.Select(s => new KhachHangViewModel()
                {
                    MAKH = s.MAKH,
                    HOTEN = s.HOTEN,
                    NGAYSINH = s.NGAYSINH,
                    GIOITINH = s.GIOITINH,
                    DIENTHOAI = s.DIENTHOAI,
                    MAIL = s.MAIL,
                    DIACHI = s.DIACHI,
                    NGAYDK = s.NGAYDK
                }).ToList<KhachHangViewModel>();
            }

            if(khachhang.Count == 0)
            {
                return NotFound();
            }

            return Ok(khachhang);
        }


        public IHttpActionResult GetKhachHangById(int id)
        {
            KhachHangViewModel khachhang = null;

            using (var ctx = new BANHANGONLINEEntities5())
            {
                khachhang = ctx.KHACHHANGs.Where(s => s.MAKH == id)
                                          .Select(s => new KhachHangViewModel()
                                          {
                                              MAKH = s.MAKH,
                                              HOTEN = s.HOTEN,
                                              NGAYSINH = s.NGAYSINH,
                                              GIOITINH = s.GIOITINH,
                                              DIENTHOAI = s.DIENTHOAI,
                                              MAIL = s.MAIL,
                                              DIACHI = s.DIACHI,
                                              NGAYDK = s.NGAYDK
                                          }).FirstOrDefault<KhachHangViewModel>();
            }

            if (khachhang == null)
            {
                return NotFound();
            }


            return Ok(khachhang);

        }


        // POST
        public IHttpActionResult PostNewKhachHang(KhachHangViewModel khachhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemKhachHang(khachhang.HOTEN, khachhang.NGAYSINH, khachhang.GIOITINH, khachhang.DIENTHOAI, khachhang.MAIL, khachhang.DIACHI, khachhang.NGAYDK);
                ctx.SaveChanges();
            }

            return Ok();
        }


        // PUT
        public IHttpActionResult PutKhachHang(KhachHangViewModel khachhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                var existingKhachHang = ctx.KHACHHANGs.Where(s => s.MAKH == khachhang.MAKH).FirstOrDefault<KHACHHANG>();

                if (existingKhachHang != null)
                {
                    ctx.sp_SuaKhachHang(khachhang.HOTEN, khachhang.NGAYSINH, khachhang.GIOITINH, khachhang.DIENTHOAI, khachhang.MAIL, khachhang.DIACHI, khachhang.NGAYDK, khachhang.MAKH);
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }



            return Ok();

        }
    }
}
