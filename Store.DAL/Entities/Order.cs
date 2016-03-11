using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DAL.Entities
{
    public class Order
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
        public virtual ClientProfile User { get; set; }

        [Display(Name = "Статус")]
        public virtual Status Status { get; set; }

        //public virtual int Delivery_Id { get; set; }

        [Display(Name = "Доставка")]
        //[ForeignKey("Delivery_Id")]
        public virtual Delivery Delivery { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}