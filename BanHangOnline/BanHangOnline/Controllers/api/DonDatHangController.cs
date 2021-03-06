﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BanHangOnline.Models;
using BanHangOnline.Models.Data;

namespace BanHangOnline.Controllers
{
    public class DonDatHangController : ApiController
    {
        public IHttpActionResult GetAllDonDatHang()
        {
            IList<DonDatHangViewModel> dondh = null;
            using (var ctx = new BANHANGONLINEEntities5())
            {
                dondh = ctx.DONDHs.Select(s => new DonDatHangViewModel()
                {
                    MADH = s.MADH,
                    MASP = s.MASP,
                    MAKH = s.MAKH,
                    MANV = s.MANV,
                    SLD = s.SLD,
                    THANHTIEN = s.THANHTIEN,
                    NGAYDH = s.NGAYDH
                }).ToList<DonDatHangViewModel>();
            }

            if(dondh.Count == 0)
            {
                return NotFound();
            }

            return Ok(dondh);
        }


        public IHttpActionResult GetDonDatHangById(int id)
        {
            DonDatHangViewModel dondh = null;

            using (var ctx = new BANHANGONLINEEntities5())
            {
                dondh = ctx.DONDHs.Where(s => s.MADH == id)
                                .Select(s => new DonDatHangViewModel()
                                {
                                    MADH = s.MADH,
                                    MASP = s.MASP,
                                    MAKH = s.MAKH,
                                    MANV = s.MANV,
                                    SLD = s.SLD,
                                    THANHTIEN = s.THANHTIEN,
                                    NGAYDH = s.NGAYDH
                                }).FirstOrDefault<DonDatHangViewModel>();
            }

            if (dondh == null)
            {
                return NotFound();
            }

            return Ok(dondh);
        }

        // POST:
        public IHttpActionResult PostNewDonDatHang(DonDatHangViewModel dondh)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invaild data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                ctx.sp_ThemDonDatHang(dondh.MASP, dondh.MAKH, dondh.MANV, dondh.SLD, dondh.THANHTIEN, dondh.NGAYDH);
                ctx.SaveChanges();
            }

            return Ok();
        }


        // PUT:
        public IHttpActionResult PutDonDatHang(DonDatHangViewModel dondh) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            using (var ctx = new BANHANGONLINEEntities5())
            {
                var existingDonDatHang = ctx.DONDHs.Where(s => s.MADH == dondh.MADH).FirstOrDefault<DONDH>();
                if (existingDonDatHang != null)
                {
                    ctx.sp_SuaDonDathang(dondh.MASP, dondh.MAKH, dondh.MANV, dondh.SLD, dondh.THANHTIEN, dondh.NGAYDH, dondh.MADH);
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
