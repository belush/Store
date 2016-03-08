using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class FilterModelDTO
    {
        public decimal PriceFrom { get; set; }
        public decimal PriceTo { get; set; }
        public int ColorId { get; set; }
        public int CategoryId { get; set; }
        public int SizeHFrom { get; set; }
        public int SizeWFrom { get; set; }
        public int SizeDFrom { get; set; }
        public int SizeHTo { get; set; }
        public int SizeWTo { get; set; }
        public int SizeDTo { get; set; }
    }
}
