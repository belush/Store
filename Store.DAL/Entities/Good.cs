using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.DAL.Entities
{
    public class Good
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Дата поступления")]
        public DateTime Date { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        [Display(Name = "Количество")]
        public int Count { get; set; }

        [Display(Name = "Ширина")]
        public int SizeWidth { get; set; }

        [Display(Name = "Высота")]
        public int SizeHeight { get; set; }

        [Display(Name = "Глубина")]
        public int SizeDepth { get; set; }

        [Display(Name = "Покупка")]
        public decimal PriceIncome { get; set; }

        [Display(Name = "Продажа")]
        public decimal PriceSale { get; set; }

        public virtual Category Category { get; set; }

        public virtual Color Color { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}