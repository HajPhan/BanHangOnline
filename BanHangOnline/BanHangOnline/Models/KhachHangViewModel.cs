using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class KhachHangViewModel
    {
        [Key]
        [Display(Name ="Mã Khách Hàng")]
        public int MAKH { get; set; }

        [Display(Name ="Họ Tên")]
        public string HOTEN { get; set; }

        [Display(Name ="Ngày Sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NGAYSINH { get; set; }

        [Display(Name ="Giới Tính")]
        public string GIOITINH { get; set; }

        [Display(Name ="Điện Thoại")]
        public string DIENTHOAI { get; set; }

        [Display(Name ="Mail")]
        public string MAIL { get; set; }

        [Display(Name ="Địa Chỉ")]
        public string DIACHI { get; set; }

        [Display(Name ="Ngày Đăng Ký")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? NGAYDK { get; set; }

    }

    public enum GenderKhachHang
    {
        nam,
        nữ,
        khác
    }
}