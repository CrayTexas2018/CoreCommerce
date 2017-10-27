namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shopify_Data : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopifyProducts",
                c => new
                    {
                        shopify_product_id = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
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
                        company_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.shopify_product_id)
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: false)
                .Index(t => t.company_id);
            
            CreateTable(
                "dbo.ShopifyImages",
                c => new
                    {
                        shopify_image_id = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        product_id = c.Int(nullable: false),
                        position = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        width = c.Int(nullable: false),
                        height = c.Int(nullable: false),
                        src = c.String(),
                        company_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.shopify_image_id)
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: false)
                .ForeignKey("dbo.ShopifyProducts", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.company_id);
            
            CreateTable(
                "dbo.ShopifyVariants",
                c => new
                    {
                        shopify_variant_id = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
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
                        image_id = c.Int(),
                        inventory_quantity = c.Int(nullable: false),
                        weight = c.String(),
                        weight_unit = c.String(),
                        old_inventory_quantity = c.Int(nullable: false),
                        requires_shipping = c.Boolean(nullable: false),
                        company_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.shopify_variant_id)
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: false)
                .ForeignKey("dbo.ShopifyImages", t => t.image_id)
                .ForeignKey("dbo.ShopifyProducts", t => t.product_id, cascadeDelete: true)
                .Index(t => t.product_id)
                .Index(t => t.image_id)
                .Index(t => t.company_id);
            
            AddColumn("dbo.Boxes", "shopify_variant_id", c => c.Int(nullable: false));
            AddColumn("dbo.Boxes", "shopify_product_shopify_product_id", c => c.Int());
            CreateIndex("dbo.Boxes", "shopify_product_shopify_product_id");
            AddForeignKey("dbo.Boxes", "shopify_product_shopify_product_id", "dbo.ShopifyProducts", "shopify_product_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boxes", "shopify_product_shopify_product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyVariants", "product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyVariants", "image_id", "dbo.ShopifyImages");
            DropForeignKey("dbo.ShopifyVariants", "company_id", "dbo.Companies");
            DropForeignKey("dbo.ShopifyImages", "product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyImages", "company_id", "dbo.Companies");
            DropForeignKey("dbo.ShopifyProducts", "company_id", "dbo.Companies");
            DropIndex("dbo.ShopifyVariants", new[] { "company_id" });
            DropIndex("dbo.ShopifyVariants", new[] { "image_id" });
            DropIndex("dbo.ShopifyVariants", new[] { "product_id" });
            DropIndex("dbo.ShopifyImages", new[] { "company_id" });
            DropIndex("dbo.ShopifyImages", new[] { "product_id" });
            DropIndex("dbo.ShopifyProducts", new[] { "company_id" });
            DropIndex("dbo.Boxes", new[] { "shopify_product_shopify_product_id" });
            DropColumn("dbo.Boxes", "shopify_product_shopify_product_id");
            DropColumn("dbo.Boxes", "shopify_variant_id");
            DropTable("dbo.ShopifyVariants");
            DropTable("dbo.ShopifyImages");
            DropTable("dbo.ShopifyProducts");
        }
    }
}
