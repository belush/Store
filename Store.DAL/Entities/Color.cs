using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.DAL.Entities
{
    public class Color
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}