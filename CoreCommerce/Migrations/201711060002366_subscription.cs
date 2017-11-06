namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "billing_address_1", c => c.String());
            AddColumn("dbo.Users", "billing_address_2", c => c.String());
            AddColumn("dbo.Users", "billing_city", c => c.String());
            AddColumn("dbo.Users", "billing_state", c => c.String());
            AddColumn("dbo.Users", "billing_zip", c => c.String());
            AddColumn("dbo.Subscriptions", "next_box_id", c => c.Int(nullable: true));
            CreateIndex("dbo.Subscriptions", "next_box_id");
            AddForeignKey("dbo.Subscriptions", "next_box_id", "dbo.Boxes", "box_id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "next_box_id", "dbo.Boxes");
            DropIndex("dbo.Subscriptions", new[] { "next_box_id" });
            DropColumn("dbo.Subscriptions", "next_box_id");
            DropColumn("dbo.Users", "billing_zip");
            DropColumn("dbo.Users", "billing_state");
            DropColumn("dbo.Users", "billing_city");
            DropColumn("dbo.Users", "billing_address_2");
            DropColumn("dbo.Users", "billing_address_1");
        }
    }
}
