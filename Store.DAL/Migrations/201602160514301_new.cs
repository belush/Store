namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "ImageType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "ImageType");
        }
    }
}
