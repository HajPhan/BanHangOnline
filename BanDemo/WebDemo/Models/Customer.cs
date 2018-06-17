using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Customer
{
    [DisplayName("Tên Đăng nhập")]
    [Required(ErrorMessage = "ban chua nhap ten dang nhap")]
    public string Id { get; set; }
    [DisplayName("Mật khẩu")]
    [Required(ErrorMessage = "ban chua nhap mat khau")]
    public string Password { get; set; }
    [DisplayName("Tên Đầy đủ")]
    [Required(ErrorMessage = "ban chua nhap ten day du")]
    public string Fullname { get; set; }
    [DisplayName("Nhập email")]
    [Required(ErrorMessage = "ban chua nhap email")]
    public string Email { get; set; }
    [DisplayName("Ảnh")]
    [Required(ErrorMessage = "ban chua nhap ten dang nhap")]
    public string Photo { get; set; }

    public bool Activated { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}