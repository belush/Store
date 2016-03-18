using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.WEB.Models.ReportViewModels;

namespace Store.WEB.Models
{
    public class ReportViewModel
    {
        public string User { get; set; }
        public List<ReportItem> Items { get; set; }
    }
}