using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }

        //public string Mobile { get; set; }

        //todo: edited
        [Display(Name = "Блокировка")]
        public bool IsBlocked { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}