namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShopifyProducts", name: "Id", newName: "product_id");
            RenameColumn(table: "dbo.ShopifyImages", name: "Id", newName: "image_id");
            RenameColumn(table: "dbo.ShopifyVariants", name: "Id", newName: "variant_id");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.ShopifyVariants", name: "variant_id", newName: "Id");
            RenameColumn(table: "dbo.ShopifyImages", name: "image_id", newName: "Id");
            RenameColumn(table: "dbo.ShopifyProducts", name: "product_id", newName: "Id");
        }
    }
}
