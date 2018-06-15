using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class LoaiSanPhamViewModel
    {
        [Key]
        [Display(Name ="Mã Loại")]
        public int MALOAI { get; set; }

        [Display(Name ="Tên Loại")]
        public string TENLOAI { get; set; }

        [Display(Name ="Đơn Vị Tính")]
        public string DVT { get; set; }
    }
}