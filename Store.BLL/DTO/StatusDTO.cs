using System.ComponentModel.DataAnnotations;

namespace Store.BLL.DTO
{
    public class StatusDTO
    {
        public int Id { get; set; }

        [Display(Name = "Статус")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }
    }
}