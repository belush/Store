namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class house : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Deliveries", "House", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Deliveries", "House", c => c.Int(nullable: false));
        }
    }
}
