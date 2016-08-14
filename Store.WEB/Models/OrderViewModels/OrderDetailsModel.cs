using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.WEB.Models.OrderViewModels
{
    public class OrderDetailsModel
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

        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}