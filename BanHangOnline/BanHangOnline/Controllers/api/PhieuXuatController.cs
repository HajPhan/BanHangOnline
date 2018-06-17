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
    public class PhieuXuatController : ApiController
    {
        public IHttpActionResult GetAllPhieuXuat()
        {
            IList<PhieuXuatViewModel> phieuxuat = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                phieuxuat = ctx.PHIEUXUATs.Select(s => new PhieuXuatViewModel()
                {
                    MAPX = s.MAPX,
                    MAKH = s.MAKH,
                    MASP = s.MASP,
                    MANV = s.MANV,
                    MADH = s.MADH,
                    SLX = s.SLX,
                    THANHTIEN = s.THANHTIEN,
                    NGAYXUAT = s.NGAYXUAT
                }).ToList<PhieuXuatViewModel>();
            }

            if(phieuxuat.Count == 0)
            {
                return NotFound();
            }

            return Ok(phieuxuat);
        }


        public IHttpActionResult GetPhieuXuatById(int id)
        {
            PhieuXuatViewModel phieuxuat = null;

            using (var ctx = new BANHANGONLINEEntities5())
            {
                phieuxuat = ctx.PHIEUXUATs.Where(s => s.MAPX == id)
                                         .Select(s => new PhieuXuatViewModel()
                                         {
                                             MAPX = s.MAPX,
                                             MAKH = s.MAKH,
                                             MASP = s.MASP,
                                             MANV = s.MANV,
                                             MADH = s.MADH,
                                             SLX = s.SLX,
                                             THANHTIEN = s.THANHTIEN,
                                             NGAYXUAT = s.NGAYXUAT

                                         }).FirstOrDefault<PhieuXuatViewModel>();
            }

            if (phieuxuat == null)
            {
                return NotFound();
            }

            return Ok(phieuxuat);
            
        }

        public IHttpActionResult PostNewPhieuXuat(PhieuXuatViewModel phieuxuat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemPhieuXuat(phieuxuat.MAKH, phieuxuat.MASP, phieuxuat.MANV, phieuxuat.MADH, phieuxuat.SLX, phieuxuat.THANHTIEN, phieuxuat.NGAYXUAT);
                ctx.SaveChanges();
            }

            return Ok();
        }



        // PUT:
        public IHttpActionResult PutPhieuXuat(PhieuXuatViewModel phieuxuat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                var existingPhieuXuat = ctx.PHIEUXUATs.Where(s => s.MAPX == phieuxuat.MAPX).FirstOrDefault<PHIEUXUAT>();

                if (existingPhieuXuat != null)
                {
                    ctx.sp_SuaPhieuXuat(phieuxuat.MASP, phieuxuat.MAKH, phieuxuat.MANV, phieuxuat.MADH, phieuxuat.SLX, phieuxuat.THANHTIEN, phieuxuat.NGAYXUAT, phieuxuat.MAPX);

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
