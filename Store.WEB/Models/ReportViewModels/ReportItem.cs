using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WEB.Models.ReportViewModels
{
    public class ReportItem
    {
        public string Good { get; set; }
        public DateTime DateSale { get; set; }
        public double Cost { get; set; }
    }
}