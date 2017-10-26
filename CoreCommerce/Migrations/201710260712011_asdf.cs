namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies");
            DropIndex("dbo.Boxes", new[] { "company_company_id" });
            DropIndex("dbo.Subscriptions", new[] { "company_company_id" });
            RenameColumn(table: "dbo.Boxes", name: "company_company_id", newName: "company_id");
            RenameColumn(table: "dbo.Subscriptions", name: "company_company_id", newName: "company_id");
            AlterColumn("dbo.Boxes", "company_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Subscriptions", "company_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Boxes", "company_id");
            CreateIndex("dbo.Subscriptions", "company_id");
            AddForeignKey("dbo.Boxes", "company_id", "dbo.Companies", "company_id", cascadeDelete: true);
            AddForeignKey("dbo.Subscriptions", "company_id", "dbo.Companies", "company_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "company_id", "dbo.Companies");
            DropForeignKey("dbo.Boxes", "company_id", "dbo.Companies");
            DropIndex("dbo.Subscriptions", new[] { "company_id" });
            DropIndex("dbo.Boxes", new[] { "company_id" });
            AlterColumn("dbo.Subscriptions", "company_id", c => c.Int());
            AlterColumn("dbo.Boxes", "company_id", c => c.Int());
            RenameColumn(table: "dbo.Subscriptions", name: "company_id", newName: "company_company_id");
            RenameColumn(table: "dbo.Boxes", name: "company_id", newName: "company_company_id");
            CreateIndex("dbo.Subscriptions", "company_company_id");
            CreateIndex("dbo.Boxes", "company_company_id");
            AddForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies", "company_id");
            AddForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies", "company_id");
        }
    }
}
