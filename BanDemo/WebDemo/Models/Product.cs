using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public partial class Product
{
    public int Id { get; set; }
    [DisplayName("Tên điện thoại")]
    [Required(ErrorMessage ="ban chua nhap ten san pham")]
    public string Name { get; set; }

    //  public string UnitBrief { get; set; }
    [DisplayName("Giá")]
    [DisplayFormat(DataFormatString = "{0:0,00.##}", ApplyFormatInEditMode = true)]
    public double UnitPrice { get; set; }
    [DisplayName("Ảnh")]
    public string Image { get; set; }
 
    public System.DateTime ProductDate { get; set; }

    //public bool Available { get; set; }
    [DisplayName("Mô tả")]
    public string Description { get; set; }
   
    public int CategoryId { get; set; }
    
    public string SupplierId { get; set; }
    [DisplayName("Số lượng")]
    public int Quantity { get; set; }
   
    public double Discount { get; set; }
   
    public bool Special { get; set; }
    
 //   public bool Latest { get; set; }
  
  //  public int Views { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    public virtual Supplier Supplier { get; set; }
}