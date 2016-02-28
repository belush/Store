namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iden : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "User_Id");
            AddForeignKey("dbo.Orders", "User_Id", "dbo.ClientProfiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "User_Id", "dbo.ClientProfiles");
            DropIndex("dbo.Orders", new[] { "User_Id" });
            DropColumn("dbo.Orders", "User_Id");
        }
    }
}
