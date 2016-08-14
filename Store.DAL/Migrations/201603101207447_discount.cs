namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class discount : DbMigration
    {
        public override void Up()
       {
            AddColumn("dbo.ClientProfiles", "Discount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientProfiles", "Discount");
        }
    }
}
