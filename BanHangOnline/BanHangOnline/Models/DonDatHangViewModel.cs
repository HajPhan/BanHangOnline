using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class DonDatHangViewModel
    {
        [Key]
        [Display(Name = "Mã Đơn Hàng")]
        public int MADH { get; set; }

        [Display(Name = "Mã Khách Hàng")]
        public int? MAKH { get; set; }

        [Display(Name = "Mã Sản Phẩm")]
        public int? MASP { get; set; }

        [Display(Name = "Mã Nhân Viên")]
        public int? MANV { get; set; }

        [Display(Name = "Số Lượng Đặt")]
        public int? SLD { get; set; }

        [Display(Name = "Thành Tiền")]
        [DisplayFormat(DataFormatString = "{0:0,00.##}", ApplyFormatInEditMode = true)]
        public decimal? THANHTIEN { get; set; }

        [Display(Name ="Ngày Đặt Hàng")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime? NGAYDH { get; set; }
    }
}