using System;
using System.Collections.Generic;

namespace Store.DAL.Entities
{
    public class Good
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ImageType { get; set; }

        public int Count { get; set; }

        //public int Size { get; set; }

        public virtual Price Price { get; set; }

        public virtual Category Category { get; set; }

        public virtual Color Color { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}