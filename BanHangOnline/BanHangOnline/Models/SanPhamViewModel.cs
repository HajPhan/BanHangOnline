using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class SanPhamViewModel
    {
        [Key]
        [Display(Name ="Mã Sản Phẩm")]
        public int MASP { get; set; }

        [Display(Name ="Tên Sản Phẩm")]
        public string TENSP { get; set; }

        [Display(Name ="Giá Tiền")]
        [DisplayFormat(DataFormatString ="{0:0,00.##}", ApplyFormatInEditMode =true)]
        public decimal? GIA { get; set; }

        [Display(Name ="Chi Tiết")]
        public string CHITIET { get; set; }

        [Display(Name ="Ảnh Sản Phẩm")]
        public string IMAGES { get; set; }

        [Display(Name ="Trạng Thái")]
        public int? TRANGTHAI { get; set; }

        [Display(Name ="Giảm Giá")]
        [DisplayFormat(DataFormatString = "{0:0,00.##}", ApplyFormatInEditMode =true)]
        public decimal? GIAMGIA { get; set; }

        [Display(Name ="Số Lượng")]
        public int? SL { get; set; }

        [Display(Name ="Mã Sản Xuất")]
        public int? MASX { get; set; }

        [Display(Name ="Mã Loại")]
        public int? MALOAI { get; set; }

        //[Display(Name ="Mã Nhân Viên")]
        //public int? MANV { get; set; }
    }


}