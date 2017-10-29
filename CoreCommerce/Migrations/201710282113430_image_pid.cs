namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image_pid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyImages", "product_id", c => c.Long(nullable: false));
            CreateIndex("dbo.ShopifyImages", "product_id");
            AddForeignKey("dbo.ShopifyImages", "product_id", "dbo.ShopifyProducts", "product_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopifyImages", "product_id", "dbo.ShopifyProducts");
            DropIndex("dbo.ShopifyImages", new[] { "product_id" });
            DropColumn("dbo.ShopifyImages", "product_id");
        }
    }
}
