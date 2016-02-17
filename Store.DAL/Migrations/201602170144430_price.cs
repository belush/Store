namespace Store.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class price : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "PriceIncome_Id", c => c.Int());
            AddColumn("dbo.Goods", "PriceSale_Id", c => c.Int());
            CreateIndex("dbo.Goods", "PriceIncome_Id");
            CreateIndex("dbo.Goods", "PriceSale_Id");
            AddForeignKey("dbo.Goods", "PriceIncome_Id", "dbo.Prices", "Id");
            AddForeignKey("dbo.Goods", "PriceSale_Id", "dbo.Prices", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Goods", "PriceSale_Id", "dbo.Prices");
            DropForeignKey("dbo.Goods", "PriceIncome_Id", "dbo.Prices");
            DropIndex("dbo.Goods", new[] { "PriceSale_Id" });
            DropIndex("dbo.Goods", new[] { "PriceIncome_Id" });
            DropColumn("dbo.Goods", "PriceSale_Id");
            DropColumn("dbo.Goods", "PriceIncome_Id");
        }
    }
}
