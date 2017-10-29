namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShopifyImages", "image_id", "dbo.ShopifyProducts");
            DropIndex("dbo.ShopifyImages", new[] { "image_id" });
            DropIndex("dbo.ShopifyVariants", new[] { "shopify_product_Id" });
            CreateIndex("dbo.ShopifyVariants", "shopify_product_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ShopifyVariants", new[] { "shopify_product_id" });
            CreateIndex("dbo.ShopifyVariants", "shopify_product_Id");
            CreateIndex("dbo.ShopifyImages", "image_id");
            AddForeignKey("dbo.ShopifyImages", "image_id", "dbo.ShopifyProducts", "product_id", cascadeDelete: true);
        }
    }
}
