using System;
using System.Collections.Generic;
using System.Linq;
using Store.BLL.DTO;
using Store.DAL.Entities;

namespace Store.WEB.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public decimal Sum
        {
            get
            {
                if (OrderItems != null)
                {
                    return OrderItems.Sum(o => o.Good.PriceSale*o.Number);
                }

                return 0;
            }
            set { Sum = value; }
        }

        public DateTime DateCreation { get; set; }

        public DateTime DateSale { get; set; }

        public User User { get; set; }

        public StatusDTO Status { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
        public List<string> OrderItemsId { get; set; }
    }
}