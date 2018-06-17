using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDemo.Areas.Admin.Models
{
    public class KhachHangViewModel
    {
        [Display (Name = "Tên Đăng nhập")]
        [Required (ErrorMessage = "ban chua nhap ten dang nhap")]
        public string Id { get; set; }
        [Display (Name = "Mật khẩu")]
        [Required (ErrorMessage = "ban chua nhap mat khau")]
        public string Password { get; set; }
        [Display (Name = "Tên Đầy đủ")]
        [Required (ErrorMessage = "ban chua nhap ten day du")]
        public string Fullname { get; set; }
        [Display (Name = "Nhập email")]
        [Required(ErrorMessage = "ban chua nhap email")]
        public string Email { get; set; }
        [Display (Name = "Ảnh")]
        [Required (ErrorMessage = "ban chua nhap ten dang nhap")]
        public string Photo { get; set; }

        public bool Activated { get; set; }
    }
}