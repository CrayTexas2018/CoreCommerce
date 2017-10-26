namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class subscriptionstuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies");
            DropIndex("dbo.Subscriptions", new[] { "company_company_id" });
            RenameColumn(table: "dbo.Subscriptions", name: "company_company_id", newName: "company_id");
            AddColumn("dbo.Subscriptions", "product_id", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "next_product_id", c => c.Int(nullable: false));
            AddColumn("dbo.Subscriptions", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Subscriptions", "next_price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Subscriptions", "product_name", c => c.String());
            AlterColumn("dbo.Subscriptions", "company_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Subscriptions", "company_id");
            AddForeignKey("dbo.Subscriptions", "company_id", "dbo.Companies", "company_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "company_id", "dbo.Companies");
            DropIndex("dbo.Subscriptions", new[] { "company_id" });
            AlterColumn("dbo.Subscriptions", "company_id", c => c.Int());
            DropColumn("dbo.Subscriptions", "product_name");
            DropColumn("dbo.Subscriptions", "next_price");
            DropColumn("dbo.Subscriptions", "price");
            DropColumn("dbo.Subscriptions", "next_product_id");
            DropColumn("dbo.Subscriptions", "product_id");
            RenameColumn(table: "dbo.Subscriptions", name: "company_id", newName: "company_company_id");
            CreateIndex("dbo.Subscriptions", "company_company_id");
            AddForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies", "company_id");
        }
    }
}
