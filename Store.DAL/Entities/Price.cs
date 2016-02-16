using System.Collections.Generic;

namespace Store.DAL.Entities
{
    public class Price
    {
        public int Id { get; set; }

        public decimal Value { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}