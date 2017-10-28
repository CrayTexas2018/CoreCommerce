namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shopify : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopifyProducts",
                c => new
                    {
                        product_id = c.Long(nullable: false),
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
                .PrimaryKey(t => t.product_id)
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: false)
                .Index(t => t.company_id);
            
            CreateTable(
                "dbo.ShopifyImages",
                c => new
                    {
                        image_id = c.Long(nullable: false),
                        variant_id = c.Long(nullable: false),
                        position = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                        width = c.Int(nullable: false),
                        height = c.Int(nullable: false),
                        src = c.String(),
                        company_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.image_id, t.variant_id })
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: false)
                .ForeignKey("dbo.ShopifyProducts", t => t.image_id, cascadeDelete: true)
                .Index(t => t.image_id)
                .Index(t => t.company_id);
            
            CreateTable(
                "dbo.ShopifyVariants",
                c => new
                    {
                        variant_id = c.Long(nullable: false),
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
                        inventory_quantity = c.Int(nullable: false),
                        weight = c.String(),
                        weight_unit = c.String(),
                        old_inventory_quantity = c.Int(nullable: false),
                        requires_shipping = c.Boolean(nullable: false),
                        company_id = c.Int(nullable: false),
                        shopify_product_Id = c.Long(),
                    })
                .PrimaryKey(t => t.variant_id)
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: false)
                .ForeignKey("dbo.ShopifyProducts", t => t.shopify_product_Id)
                .Index(t => t.variant_id, unique: true)
                .Index(t => t.company_id)
                .Index(t => t.shopify_product_Id);
            
            AddColumn("dbo.Boxes", "shopify_product_id", c => c.Long(nullable: false));
            AddColumn("dbo.Boxes", "shopify_variant_id", c => c.Long(nullable: false));
            CreateIndex("dbo.Boxes", "shopify_product_id");
            CreateIndex("dbo.Boxes", "shopify_variant_id");
            AddForeignKey("dbo.Boxes", "shopify_product_id", "dbo.ShopifyProducts", "product_id", cascadeDelete: true);
            AddForeignKey("dbo.Boxes", "shopify_variant_id", "dbo.ShopifyVariants", "variant_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boxes", "shopify_variant_id", "dbo.ShopifyVariants");
            DropForeignKey("dbo.Boxes", "shopify_product_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyVariants", "shopify_product_Id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyVariants", "company_id", "dbo.Companies");
            DropForeignKey("dbo.ShopifyImages", "image_id", "dbo.ShopifyProducts");
            DropForeignKey("dbo.ShopifyImages", "company_id", "dbo.Companies");
            DropForeignKey("dbo.ShopifyProducts", "company_id", "dbo.Companies");
            DropIndex("dbo.ShopifyVariants", new[] { "shopify_product_Id" });
            DropIndex("dbo.ShopifyVariants", new[] { "company_id" });
            DropIndex("dbo.ShopifyVariants", new[] { "variant_id" });
            DropIndex("dbo.ShopifyImages", new[] { "company_id" });
            DropIndex("dbo.ShopifyImages", new[] { "image_id" });
            DropIndex("dbo.ShopifyProducts", new[] { "company_id" });
            DropIndex("dbo.Boxes", new[] { "shopify_variant_id" });
            DropIndex("dbo.Boxes", new[] { "shopify_product_id" });
            DropColumn("dbo.Boxes", "shopify_variant_id");
            DropColumn("dbo.Boxes", "shopify_product_id");
            DropTable("dbo.ShopifyVariants");
            DropTable("dbo.ShopifyImages");
            DropTable("dbo.ShopifyProducts");
        }
    }
}
