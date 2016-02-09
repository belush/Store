using System;

namespace Store.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public bool IsBlocked { get; set; }
    }
}
