namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stripe1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "stripe_key", c => c.String());
            AddColumn("dbo.Boxes", "stipe_plan_id", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_first_name", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_last_name", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_address_1", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_address_2", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_city", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_state", c => c.String());
            AddColumn("dbo.Subscriptions", "billing_zip", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "checkout_id", c => c.Int());
            CreateIndex("dbo.Subscriptions", "checkout_id");
            AddForeignKey("dbo.Subscriptions", "checkout_id", "dbo.Checkouts", "checkout_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "checkout_id", "dbo.Checkouts");
            DropIndex("dbo.Subscriptions", new[] { "checkout_id" });
            DropColumn("dbo.Subscriptions", "checkout_id");
            DropColumn("dbo.Subscriptions", "billing_zip");
            DropColumn("dbo.Subscriptions", "billing_state");
            DropColumn("dbo.Subscriptions", "billing_city");
            DropColumn("dbo.Subscriptions", "billing_address_2");
            DropColumn("dbo.Subscriptions", "billing_address_1");
            DropColumn("dbo.Subscriptions", "billing_last_name");
            DropColumn("dbo.Subscriptions", "billing_first_name");
            DropColumn("dbo.Boxes", "stipe_plan_id");
            DropColumn("dbo.Companies", "stripe_key");
        }
    }
}
