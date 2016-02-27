using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Mobile { get; set; }

        public bool IsBlocked { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}