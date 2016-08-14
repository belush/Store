namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeliveryId", c => c.Int(nullable: false));
            AddColumn("dbo.Deliveries", "OrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "OrderId");
            DropColumn("dbo.Orders", "DeliveryId");
        }
    }
}
