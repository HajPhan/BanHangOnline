//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BanHangOnline.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHIEUXUAT
    {
        public int MAPX { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<int> MASP { get; set; }
        public Nullable<int> MANV { get; set; }
        public Nullable<int> MADH { get; set; }
        public Nullable<int> SLX { get; set; }
        public Nullable<decimal> THANHTIEN { get; set; }
        public Nullable<System.DateTime> NGAYXUAT { get; set; }
    
        public virtual DONDH DONDH { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
