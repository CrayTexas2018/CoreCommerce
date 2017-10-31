namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subtablechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "stripe_plan_id", c => c.String());
            AddColumn("dbo.Subscriptions", "box_name", c => c.String());
            AddColumn("dbo.Subscriptions", "shopify_product_id", c => c.Long(nullable: false));
            AddColumn("dbo.Subscriptions", "shopify_product_title", c => c.String());
            AddColumn("dbo.Subscriptions", "shopify_product_body", c => c.String());
            AddColumn("dbo.Subscriptions", "shopify_variant_id", c => c.Long(nullable: false));
            AddColumn("dbo.Subscriptions", "shopify_variant_price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Subscriptions", "shopify_variant_sku", c => c.String());
            AddColumn("dbo.Subscriptions", "next_charge_amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Subscriptions", "box_id");
            CreateIndex("dbo.Subscriptions", "shopify_product_id");
            CreateIndex("dbo.Subscriptions", "shopify_variant_id");
            AddForeignKey("dbo.Subscriptions", "box_id", "dbo.Boxes", "box_id", cascadeDelete: false);
            AddForeignKey("dbo.Subscriptions", "shopify_product_id", "dbo.ShopifyProducts", "product_id", cascadeDelete: false);
            AddForeignKey("dbo.Subscriptions", "shopify_variant_id", "dbo.ShopifyVariants", "variant_id", cascadeDelete: false);
            DropColumn("dbo.Subscriptions", "stripe_id");
            DropColumn("dbo.Subscriptions", "next_box_id");
            DropColumn("dbo.Subscriptions", "price");
            DropColumn("dbo.Subscriptions", "next_price");
            DropColumn("dbo.Subscriptions", "product_name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "product_name", c => c.String());
            AddColumn("dbo.Subscriptions", "next_price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Subscriptions", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Subscriptions", "next_box_id", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "stripe_id", c => c.String());
            DropForeignKey("dbo.Subscriptions", "shopify_variant_id", "dbo.ShopifyVariants");
            DropForeignKey("dbo.Subscriptions", "shopify_product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.Subscriptions", "box_id", "dbo.Boxes");
            DropIndex("dbo.Subscriptions", new[] { "shopify_variant_id" });
            DropIndex("dbo.Subscriptions", new[] { "shopify_product_id" });
            DropIndex("dbo.Subscriptions", new[] { "box_id" });
            DropColumn("dbo.Subscriptions", "next_charge_amount");
            DropColumn("dbo.Subscriptions", "shopify_variant_sku");
            DropColumn("dbo.Subscriptions", "shopify_variant_price");
            DropColumn("dbo.Subscriptions", "shopify_variant_id");
            DropColumn("dbo.Subscriptions", "shopify_product_body");
            DropColumn("dbo.Subscriptions", "shopify_product_title");
            DropColumn("dbo.Subscriptions", "shopify_product_id");
            DropColumn("dbo.Subscriptions", "box_name");
            DropColumn("dbo.Subscriptions", "stripe_plan_id");
        }
    }
}
