using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace Store.WEB.Models.GoodViewModels
{
    public class GoodListViewModel
    {
        public IPagedList PageList { get; set; }
        public IEnumerable<GoodViewModel> Goods { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}