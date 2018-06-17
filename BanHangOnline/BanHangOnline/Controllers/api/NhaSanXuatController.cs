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
    public class NhaSanXuatController : ApiController
    {
        public IHttpActionResult GetAllNhaSanXuat()
        {
            IList<NhaSanXuatViewModel> nhasx = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                nhasx = ctx.NHASANXUATs.Select(s => new NhaSanXuatViewModel()
                {
                    MASX = s.MASX,
                    TENSX = s.TENSX,
                    QUOCGIA = s.QUOCGIA
                }).ToList<NhaSanXuatViewModel>();
            }

            if(nhasx.Count == 0)
            {
                return NotFound();
            }

            return Ok(nhasx);
        }

        public IHttpActionResult GetNhaSanXuatById(int id)
        {
            NhaSanXuatViewModel nhasx = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                nhasx = ctx.NHASANXUATs.Where(s => s.MASX == id)
                                       .Select(s => new NhaSanXuatViewModel()
                                       {
                                           MASX = s.MASX,
                                           TENSX = s.TENSX,
                                           QUOCGIA = s.QUOCGIA
                                       }).FirstOrDefault<NhaSanXuatViewModel>();
            }

            if (nhasx == null)
            {
                return NotFound();
            }

            return Ok(nhasx);

        }

        public IHttpActionResult PostNewNhaSanXuat(NhaSanXuatViewModel nhasx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemNhaSanXuat(nhasx.TENSX, nhasx.QUOCGIA);
                ctx.SaveChanges();
            }

            return Ok();
        }


        // PUT
        public IHttpActionResult PutNhaSanXuat(NhaSanXuatViewModel nhasx)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                var exitstingNhaSx = ctx.NHASANXUATs.Where(s => s.MASX == nhasx.MASX).FirstOrDefault<NHASANXUAT>();

                if (exitstingNhaSx != null)
                {
                    ctx.sp_SuaNhaSanXuat(nhasx.TENSX, nhasx.QUOCGIA, nhasx.MASX);
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
