namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boxes", "shopify_variant_id", "dbo.ShopifyVariants");
            DropIndex("dbo.Boxes", new[] { "shopify_variant_id" });
            AlterColumn("dbo.Boxes", "shopify_variant_id", c => c.Long());
            CreateIndex("dbo.Boxes", "shopify_variant_id");
            AddForeignKey("dbo.Boxes", "shopify_variant_id", "dbo.ShopifyVariants", "variant_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boxes", "shopify_variant_id", "dbo.ShopifyVariants");
            DropIndex("dbo.Boxes", new[] { "shopify_variant_id" });
            AlterColumn("dbo.Boxes", "shopify_variant_id", c => c.Long(nullable: false));
            CreateIndex("dbo.Boxes", "shopify_variant_id");
            AddForeignKey("dbo.Boxes", "shopify_variant_id", "dbo.ShopifyVariants", "variant_id", cascadeDelete: true);
        }
    }
}
