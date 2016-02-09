using System.Data.Entity;
using Store.DAL.Entities;

namespace Store.DAL.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext() : base("StoreConnection")
        { }

        public DbSet<Category> Categories { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Good> Goods { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get; }
        public DbSet<Price> Prices { set; get; }
        public DbSet<Status> Statuses { set; get; }
        public DbSet<User> Users { set; get; }
    }
}