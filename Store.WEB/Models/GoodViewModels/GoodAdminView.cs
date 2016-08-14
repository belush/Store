using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.WEB.Models
{
    public class GoodAdminView
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Кол-во")]
        public int Count { get; set; }

        [Display(Name = "")]
        public byte[] Image { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Добавление")]
        public DateTime Date { get; set; }

        [Display(Name = "Размер")]
        public string Size { get; set; }

        [Display(Name = "Покупка")]
        public decimal PriceIncome { get; set; }

        [Display(Name = "Продажа")]
        public decimal PriceSale { get; set; }

        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }
    }
}