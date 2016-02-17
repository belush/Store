namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class price2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "SizeWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Goods", "SizeHeight", c => c.Int(nullable: false));
            AddColumn("dbo.Goods", "SizeDepth", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "SizeDepth");
            DropColumn("dbo.Goods", "SizeHeight");
            DropColumn("dbo.Goods", "SizeWidth");
        }
    }
}
