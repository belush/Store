using System;
using System.Collections.Generic;

namespace Store.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateSale { get; set; }

        //public virtual User User { get; set; }

        public virtual ClientProfile User { get; set; }

        public virtual Status Status { get; set; }

        public virtual IEnumerable<OrderItem> OrderItems { get; set; }
    }
}