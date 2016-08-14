namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Goods", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Colors", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Status", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Status", "IsDeleted");
            DropColumn("dbo.Colors", "IsDeleted");
            DropColumn("dbo.Goods", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
        }
    }
}
