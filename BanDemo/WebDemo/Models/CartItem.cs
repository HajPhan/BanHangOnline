
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDemo.Models
{
    [Serializable]
    public class CartItem
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}