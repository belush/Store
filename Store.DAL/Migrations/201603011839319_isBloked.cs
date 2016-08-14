namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isBloked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientProfiles", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientProfiles", "IsBlocked");
        }
    }
}
