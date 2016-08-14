using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DAL.Entities
{
    public class Delivery
    {
        [Display(Name = "ID")]
        [ForeignKey("Order")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите как вас зовут")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите страну")]
        [Display(Name = "Страна")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите улицу")]
        [Display(Name = "Улица")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Укажите номер дома")]
        [Display(Name = "Дом")]
        public string House { get; set; }

        [Required(ErrorMessage = "Укажите номер квартиры")]
        [Display(Name = "Квартира")]
        public int Flat { get; set; }

        [Required(ErrorMessage = "Укажите номер мобильного телефона")]
        [Display(Name = "Мобильный телефон")]
        public string Phone { get; set; }

        //public int Order_Id { get; set; }

        [Display(Name = "Заказ")]
        //[ForeignKey("Order_Id")]
        public virtual Order Order { get; set; }
    }
}