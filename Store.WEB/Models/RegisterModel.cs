using System.ComponentModel.DataAnnotations;

namespace Store.WEB.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Error")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символов")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
    }
}