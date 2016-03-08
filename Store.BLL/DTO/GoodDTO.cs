using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class GoodDTO
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

        public bool IsDeleted { get; set; }

        public virtual CategoryDTO Category { get; set; }

        public virtual ColorDTO Color { get; set; }

        public virtual ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
