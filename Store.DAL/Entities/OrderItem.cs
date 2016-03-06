using System.ComponentModel.DataAnnotations;

namespace Store.DAL.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Display(Name = "Цена")]
        public decimal PriceSale { get; set; }

        [Display(Name = "Количество")]
        public int Number { get; set; }

        public virtual Good Good { get; set; }

        public virtual Order Order { get; set; }
    }
}
