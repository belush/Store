using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.DAL.Entities
{
    public class Status
    {
        public int Id { get; set; }

        [Display(Name = "Статус")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}