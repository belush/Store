namespace Store.WEB.Models
{
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