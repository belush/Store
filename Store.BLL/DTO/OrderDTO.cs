using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.DAL.Entities;

namespace Store.BLL.DTO
{
    public class OrderDTO
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Сумма")]
        public decimal Sum { get; set; }

        [Display(Name = "Продажа")]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Продажа")]
        public DateTime DateSale { get; set; }

        [Display(Name = "Покупатель")]
        public virtual UserDTO User { get; set; }

        [Display(Name = "Статус")]
        public virtual StatusDTO Status { get; set; }

        [Display(Name = "Доставка")]
        public virtual DeliveryDTO Delivery { get; set; }

        public virtual IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
