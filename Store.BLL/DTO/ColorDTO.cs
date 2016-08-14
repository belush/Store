using System.ComponentModel.DataAnnotations;

namespace Store.BLL.DTO
{
    public class ColorDTO
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}