using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //public int Order_Id { get; set; }

        //[ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }
    }
}