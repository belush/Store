using System.ComponentModel.DataAnnotations;

namespace Store.BLL.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }

        [Display(Name = "Цена")]
        public decimal PriceSale { get; set; }

        [Display(Name = "Количество")]
        public int Number { get; set; }

        public virtual GoodDTO Good { get; set; }

        //public int OrderId { get; set; }

        public virtual OrderDTO Order { get; set; }
    }
}