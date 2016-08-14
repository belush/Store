using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.BLL;

namespace Store.WEB.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}