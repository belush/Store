using System.ComponentModel.DataAnnotations;

namespace Store.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Display(Name = "Скидака")]
        [Range(0, 100, ErrorMessage = "от 0% до 100%")]
        public double Discount { get; set; }

        //public string Mobile { get; set; }

        //todo: edited
        [Display(Name = "IsBlocked")]
        public bool IsBlocked { get; set; }

        public string Role { get; set; }
    }
}