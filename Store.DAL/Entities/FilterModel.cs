using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities
{
    //todo: where it should be?
    public class FilterModel
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public int ColorId { get; set; }
        public int CategoryId { get; set; }
        public int SizeH { get; set; }
        public int SizeW { get; set; }
        public int SizeD { get; set; }
    }
}
