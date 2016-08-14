using Store.DAL.Entities;

namespace Store.WEB.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        //TODO: is it need?
        public decimal PriceSale { get; set; }

        public int Number { get; set; }

        public int GoodId { get; set; }

        public virtual Good Good { get; set; }

        //TODO: is it need?
        public virtual Order Order { get; set; }
    }
}