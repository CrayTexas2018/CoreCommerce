namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shopify_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopifyProducts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        body_html = c.String(),
                        vendor = c.String(),
                        product_type = c.String(),
                        created_at = c.DateTime(nullable: false),
                        handle = c.String(),
                        updated_at = c.DateTime(nullable: false),
                        published_at = c.DateTime(nullable: false),
                        template_suffix = c.String(),
                        published_scope = c.String(),
                        tags = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ShopifyVariants",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        title = c.String(),
                        price = c.String(),
                        sku = c.String(),
                        position = c.Int(nullable: false),
                        inventory_policy = c.String(),
                        compare_at_price = c.String(),
                        fulfillment_service = c.String(),
                        inventory_management = c.String(),
                        option1 = c.String(),
                        option2 = c.String(),
                        option3 = c.String(),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        taxable = c.Boolean(nullable: false),
                        barcode = c.String(),
                        grams = c.Int(nullable: false),
                        image_id = c.Int(nullable: false),
                        inventory_quantity = c.Int(nullable: false),
                        weight = c.Int(nullable: false),
                        weight_unit = c.Int(nullable: false),
                        old_inventory_quantity = c.Int(nullable: false),
                        requires_shipping = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ShopifyProducts", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id);
            
            CreateTable(
                "dbo.ShopifyImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_id = c.Int(nullable: false),
                        position = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        width = c.Int(nullable: false),
                        height = c.Int(nullable: false),
                        src = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ShopifyProducts", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id);
            
            AddColumn("dbo.Boxes", "shopify_variant_id", c => c.Int(nullable: false));
            AddColumn("dbo.Boxes", "shopify_product_id", c => c.Int());
            CreateIndex("dbo.Boxes", "shopify_product_id");
            AddForeignKey("dbo.Boxes", "shopify_product_id", "dbo.ShopifyProducts", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopifyImages", "product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.Boxes", "shopify_product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyVariants", "product_id", "dbo.ShopifyProducts");
            DropIndex("dbo.ShopifyImages", new[] { "product_id" });
            DropIndex("dbo.ShopifyVariants", new[] { "product_id" });
            DropIndex("dbo.Boxes", new[] { "shopify_product_id" });
            DropColumn("dbo.Boxes", "shopify_product_id");
            DropColumn("dbo.Boxes", "shopify_variant_id");
            DropTable("dbo.ShopifyImages");
            DropTable("dbo.ShopifyVariants");
            DropTable("dbo.ShopifyProducts");
        }
    }
}
