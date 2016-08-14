namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "DeliveryId");
            DropColumn("dbo.Deliveries", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deliveries", "OrderId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "DeliveryId", c => c.Int(nullable: false));
        }
    }
}
