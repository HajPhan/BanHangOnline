using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Models
{

    public partial class Contact
    {
        public int ID { get; set; }

       
        public string Content { get; set; }

        public bool? Status { get; set; }
    }
}