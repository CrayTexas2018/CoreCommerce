namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShopifyVariants", name: "shopify_product_id", newName: "product_id");
            RenameIndex(table: "dbo.ShopifyVariants", name: "IX_shopify_product_id", newName: "IX_product_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShopifyVariants", name: "IX_product_id", newName: "IX_shopify_product_id");
            RenameColumn(table: "dbo.ShopifyVariants", name: "product_id", newName: "shopify_product_id");
        }
    }
}
