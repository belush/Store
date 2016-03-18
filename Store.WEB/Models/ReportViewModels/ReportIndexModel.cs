using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.WEB.Models.ReportViewModels
{
    public class ReportIndexModel
    {
        public string User { get; set; }
        public string Good { get; set; }
        public DateTime DateSale { get; set; }
        public double Cost { get; set; }
    }
}