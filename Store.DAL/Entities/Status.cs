using System.Collections.Generic;

namespace Store.DAL.Entities
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
