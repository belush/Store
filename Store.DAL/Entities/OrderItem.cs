namespace Store.DAL.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        public decimal PriceSale { get; set; }

        public int Number { get; set; }

        public virtual Good Good { get; set; }

        public virtual Order Order { get; set; }
    }
}
