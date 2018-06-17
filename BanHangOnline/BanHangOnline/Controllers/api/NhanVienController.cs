using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BanHangOnline.Models.Data;
using BanHangOnline.Models;

namespace BanHangOnline.Controllers
{
    public class NhanVienController : ApiController
    {
        // GET: 
        public IHttpActionResult GetAllNhanVien()
        {
            IList<NhanVienViewModel> nhanvien = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                nhanvien = ctx.NHANVIENs.Select(s => new NhanVienViewModel()
                {
                    MANV = s.MANV,
                    HOTEN = s.HOTEN,
                    NGAYSINH = s.NGAYSINH,
                    GIOITINH = s.GIOITINH,
                    DIENTHOAI = s.DIENTHOAI,
                    MAIL = s.MAIL,
                    DIACHI = s.DIACHI,
                    NGAYLAM = s.NGAYLAM,
                    LUONG = s.LUONG,
                    USERNAMES = s.USERNAMES,
                    PASSWORDS = s.PASSWORDS,
                    TRANGTHAI = s.TRANGTHAI
                }).ToList<NhanVienViewModel>();
            }

            if (nhanvien.Count == 0)
            {
                return NotFound();
            }

            return Ok(nhanvien);
        }


        public IHttpActionResult GetNhanVienById(int id)
        {
            NhanVienViewModel nhanvien = null;

            using (var ctx = new BANHANGONLINEEntities5())
            {
                nhanvien = ctx.NHANVIENs.Where(s => s.MANV == id)
                              .Select(s => new NhanVienViewModel()
                              {
                                  MANV = s.MANV,
                                  HOTEN = s.HOTEN,
                                  NGAYSINH = s.NGAYSINH,
                                  GIOITINH = s.GIOITINH,
                                  DIENTHOAI = s.DIENTHOAI,
                                  MAIL = s.MAIL,
                                  DIACHI = s.DIACHI,
                                  NGAYLAM = s.NGAYLAM,
                                  LUONG = s.LUONG,
                                  USERNAMES = s.USERNAMES,
                                  PASSWORDS = s.PASSWORDS,
                                  TRANGTHAI = s.TRANGTHAI
                              }).FirstOrDefault<NhanVienViewModel>();
            }

            if (nhanvien == null)
            {
                return NotFound();
            }

            return Ok(nhanvien);
        }


        //POST
        public IHttpActionResult PostNewNhanVien(NhanVienViewModel nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }

            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemNhanVien(nhanvien.HOTEN, nhanvien.NGAYLAM, nhanvien.GIOITINH, nhanvien.DIENTHOAI, nhanvien.MAIL, nhanvien.DIACHI, nhanvien.NGAYLAM, nhanvien.LUONG, nhanvien.USERNAMES, nhanvien.PASSWORDS, nhanvien.TRANGTHAI);
                ctx.SaveChanges();
            }

            return Ok();
        }

        // PUT
        public IHttpActionResult PutNhanVien(NhanVienViewModel nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }

            using (var ctx = new BANHANGONLINEEntities5())
            {
                var exitstingNhanvien = ctx.NHANVIENs.Where(sv => sv.MANV == nhanvien.MANV).FirstOrDefault<NHANVIEN>();

                if (exitstingNhanvien != null)
                {
                    ctx.sp_SuaNhanVien(nhanvien.HOTEN, nhanvien.NGAYSINH, nhanvien.GIOITINH, nhanvien.DIENTHOAI, nhanvien.MAIL, nhanvien.DIACHI, nhanvien.NGAYLAM, nhanvien.LUONG, nhanvien.USERNAMES, nhanvien.PASSWORDS, nhanvien.TRANGTHAI, nhanvien.MANV);
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
