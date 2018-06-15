using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanHangOnline.Models
{
    public class NhanVienViewModel
    {
        [Key]
        [Display(Name ="Mã Nhân Viên")]
        public int MANV { get; set; }

        [Display(Name ="Họ Tên")]
        public string HOTEN { get; set; }

        [Display(Name ="Ngày Sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NGAYSINH { get; set; }

        [Display(Name ="Giới Tính")]
        public string GIOITINH { get; set; }

        [Display(Name ="Điện Thoại")]
        public string DIENTHOAI { get; set; }

        [Display(Name ="Mail")]
        public string MAIL { get; set; }

        [Display(Name ="Địa Chỉ")]
        public string DIACHI { get; set; }

        [Display(Name ="Ngày Làm")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? NGAYLAM { get; set; }

        [Display(Name ="Lương")]
        [DisplayFormat(DataFormatString = "{0:0,00.##}", ApplyFormatInEditMode = true)]
        public decimal? LUONG { get; set; }

        [Display(Name ="Username")]
        public string USERNAMES { get; set; }

        [Display(Name ="Password")]
        public string PASSWORDS { get; set; }

        [Display(Name ="Trạng Thái")]
        public int? TRANGTHAI { get; set; }
    }

    public enum GenderNhanVien
    {
        nam,
        nữ,
        khác
    }
}