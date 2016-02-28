using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;

namespace Store.DAL.Context
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        private static readonly Lazy<StoreContext> _lazyStoreContext = new Lazy<StoreContext>(() => new StoreContext());

        //DbContext,
        public StoreContext() : base("StoreConnection")
        {
        }

        public StoreContext(string conectionString) : base(conectionString)
        {
        }

        public static StoreContext StoreContextInstance => _lazyStoreContext.Value;

        public DbSet<Category> Categories { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Good> Goods { set; get; }
        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get; }
        public DbSet<Price> Prices { set; get; }
        public DbSet<Status> Statuses { set; get; }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        //public DbSet<User> Users { set; get; }
    }
}