namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shopifystuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "stripe_id", c => c.String());
            AddColumn("dbo.Users", "shopify_id", c => c.Long(nullable: false));
            AddColumn("dbo.Orders", "shopify_id", c => c.Long(nullable: false));
            AddColumn("dbo.Orders", "stripe_id", c => c.String());
            AddColumn("dbo.Subscriptions", "stripe_id", c => c.String());
            AddColumn("dbo.Subscriptions", "box_id", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "next_box_id", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "provider_id");
            DropColumn("dbo.Subscriptions", "product_id");
            DropColumn("dbo.Subscriptions", "next_product_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subscriptions", "next_product_id", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "product_id", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "provider_id", c => c.Int(nullable: false));
            DropColumn("dbo.Subscriptions", "next_box_id");
            DropColumn("dbo.Subscriptions", "box_id");
            DropColumn("dbo.Subscriptions", "stripe_id");
            DropColumn("dbo.Orders", "stripe_id");
            DropColumn("dbo.Orders", "shopify_id");
            DropColumn("dbo.Users", "shopify_id");
            DropColumn("dbo.Users", "stripe_id");
        }
    }
}
