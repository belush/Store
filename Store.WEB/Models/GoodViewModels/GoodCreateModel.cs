using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Store.WEB.Models
{
    public class GoodCreateModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 50 символов")]
        public string Name { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Изображение")]
        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        [Display(Name = "Количество")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Недопустимое количество")]
        public int Count { get; set; }

        [Display(Name = "Ширина")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Недопустимая ширина")]
        public int SizeWidth { get; set; }

        [Display(Name = "Высота")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Недопустимая высота")]
        public int SizeHeight { get; set; }

        [Display(Name = "Глубина")]
        [Required]
        [Range(1, 1000, ErrorMessage = "Недопустимая глуюина")]
        public int SizeDepth { get; set; }

        [Display(Name = "Цена закупки")]
        [Required]
        [Range(1, 1000000, ErrorMessage = "Недопустимая сумма")]
        public decimal PriceIncome { get; set; }

        [Display(Name = "Цена продажи")]
        [Required]
        [Range(1, 1000000, ErrorMessage = "Недопустимая сумма")]
        public decimal PriceSale { get; set; }

        [Display(Name = "Категория")]
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Цвет")]
        [Required]
        public int ColorId { get; set; }

        [Display(Name = "Описание")]
        [StringLength(1000, MinimumLength = 0, ErrorMessage = "Длина строки должна быть до 1000 символов")]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }
    }
}