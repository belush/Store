namespace Store.DAL.Entities
{
    //todo: where it should be?
    public class FilterModel
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