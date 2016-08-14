using System;
using System.ComponentModel.DataAnnotations;

namespace Store.WEB.Models.OrderViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Сумма")]
        public decimal Sum { get; set; }

        [Display(Name = "Продажа")]
        public DateTime DateCreation { get; set; }

        [Display(Name = "Продажа")]
        public string DateSale { get; set; }

        [Display(Name = "Покупатель")]
        public string User { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }
    }
}