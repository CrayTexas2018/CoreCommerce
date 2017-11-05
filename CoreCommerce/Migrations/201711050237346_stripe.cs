namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stripe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "stripe_subscription_id", c => c.String());
            AddColumn("dbo.Subscriptions", "stripe_coupon_id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "stripe_coupon_id");
            DropColumn("dbo.Subscriptions", "stripe_subscription_id");
        }
    }
}
