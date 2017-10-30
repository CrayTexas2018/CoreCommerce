namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkout : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checkouts", "url", c => c.String());
            AddColumn("dbo.Checkouts", "is_completed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Checkouts", "completed", c => c.DateTime(nullable: false));
            AddColumn("dbo.Checkouts", "deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Checkouts", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "checkout_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Checkouts", "company_id");
            CreateIndex("dbo.Orders", "checkout_id");
            AddForeignKey("dbo.Checkouts", "company_id", "dbo.Companies", "company_id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "checkout_id", "dbo.Checkouts", "checkout_id", cascadeDelete: true);
            DropColumn("dbo.Orders", "initial_url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "initial_url", c => c.String());
            DropForeignKey("dbo.Orders", "checkout_id", "dbo.Checkouts");
            DropForeignKey("dbo.Checkouts", "company_id", "dbo.Companies");
            DropIndex("dbo.Orders", new[] { "checkout_id" });
            DropIndex("dbo.Checkouts", new[] { "company_id" });
            DropColumn("dbo.Orders", "checkout_id");
            DropColumn("dbo.Checkouts", "company_id");
            DropColumn("dbo.Checkouts", "deleted");
            DropColumn("dbo.Checkouts", "completed");
            DropColumn("dbo.Checkouts", "is_completed");
            DropColumn("dbo.Checkouts", "url");
        }
    }
}
