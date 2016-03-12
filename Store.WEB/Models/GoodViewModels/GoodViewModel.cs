using System;

namespace Store.WEB.Models
{
    public class GoodViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public int Count { get; set; }

        public int SizeWidth { get; set; }

        public int SizeHeight { get; set; }

        public int SizeDepth { get; set; }

        public decimal PriceWithDiscount { get; set; }

        public decimal PriceSale { get; set; }

        public string Category { get; set; }

        public string Color { get; set; }
    }
}