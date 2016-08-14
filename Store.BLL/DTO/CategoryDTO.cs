using System.ComponentModel.DataAnnotations;

namespace Store.BLL.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Display(Name = "Категория")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        //public virtual ICollection<Good> Goods { get; set; }
    }
}