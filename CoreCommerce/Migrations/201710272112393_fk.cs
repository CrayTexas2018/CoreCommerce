namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShopifyVariants", "image_id", "dbo.ShopifyImages");
            DropForeignKey("dbo.ShopifyImages", "product_id", "dbo.ShopifyProducts");
            DropIndex("dbo.ShopifyImages", new[] { "product_id" });
            DropIndex("dbo.ShopifyVariants", new[] { "image_id" });
            RenameColumn(table: "dbo.ShopifyImages", name: "product_id", newName: "Product_shopify_product_id");
            RenameColumn(table: "dbo.ShopifyVariants", name: "product_id", newName: "shopify_product_id");
            RenameIndex(table: "dbo.ShopifyVariants", name: "IX_product_id", newName: "IX_shopify_product_id");
            AlterColumn("dbo.ShopifyImages", "Product_shopify_product_id", c => c.Int());
            CreateIndex("dbo.ShopifyImages", "Product_shopify_product_id");
            AddForeignKey("dbo.ShopifyImages", "Product_shopify_product_id", "dbo.ShopifyProducts", "shopify_product_id");
            DropColumn("dbo.ShopifyVariants", "image_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopifyVariants", "image_id", c => c.Int());
            DropForeignKey("dbo.ShopifyImages", "Product_shopify_product_id", "dbo.ShopifyProducts");
            DropIndex("dbo.ShopifyImages", new[] { "Product_shopify_product_id" });
            AlterColumn("dbo.ShopifyImages", "Product_shopify_product_id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.ShopifyVariants", name: "IX_shopify_product_id", newName: "IX_product_id");
            RenameColumn(table: "dbo.ShopifyVariants", name: "shopify_product_id", newName: "product_id");
            RenameColumn(table: "dbo.ShopifyImages", name: "Product_shopify_product_id", newName: "product_id");
            CreateIndex("dbo.ShopifyVariants", "image_id");
            CreateIndex("dbo.ShopifyImages", "product_id");
            AddForeignKey("dbo.ShopifyImages", "product_id", "dbo.ShopifyProducts", "shopify_product_id", cascadeDelete: true);
            AddForeignKey("dbo.ShopifyVariants", "image_id", "dbo.ShopifyImages", "shopify_image_id");
        }
    }
}
