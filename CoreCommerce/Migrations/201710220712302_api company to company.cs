namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class apicompanytocompany : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ApiUsers", name: "company_api_company_id", newName: "company_company_id");
            RenameColumn(table: "dbo.Users", name: "company_api_company_id", newName: "company_company_id");
            RenameIndex(table: "dbo.ApiUsers", name: "IX_company_api_company_id", newName: "IX_company_company_id");
            RenameIndex(table: "dbo.Users", name: "IX_company_api_company_id", newName: "IX_company_company_id");
            AddColumn("dbo.Boxes", "company_company_id", c => c.Int());
            AddColumn("dbo.Companies", "Username", c => c.String());
            AddColumn("dbo.Companies", "password", c => c.String());
            AddColumn("dbo.Orders", "company_company_id", c => c.Int());
            AddColumn("dbo.Subscriptions", "company_company_id", c => c.Int());
            CreateIndex("dbo.Boxes", "company_company_id");
            CreateIndex("dbo.Orders", "company_company_id");
            CreateIndex("dbo.Subscriptions", "company_company_id");
            AddForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies", "company_id");
            AddForeignKey("dbo.Orders", "company_company_id", "dbo.Companies", "company_id");
            AddForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies", "company_id");
            DropTable("dbo.ApiCompanies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApiCompanies",
                c => new
                    {
                        api_company_id = c.Int(nullable: false, identity: true),
                        company_name = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.api_company_id);
            
            DropForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.Orders", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies");
            DropIndex("dbo.Subscriptions", new[] { "company_company_id" });
            DropIndex("dbo.Orders", new[] { "company_company_id" });
            DropIndex("dbo.Boxes", new[] { "company_company_id" });
            DropColumn("dbo.Subscriptions", "company_company_id");
            DropColumn("dbo.Orders", "company_company_id");
            DropColumn("dbo.Companies", "password");
            DropColumn("dbo.Companies", "Username");
            DropColumn("dbo.Boxes", "company_company_id");
            RenameIndex(table: "dbo.Users", name: "IX_company_company_id", newName: "IX_company_api_company_id");
            RenameIndex(table: "dbo.ApiUsers", name: "IX_company_company_id", newName: "IX_company_api_company_id");
            RenameColumn(table: "dbo.Users", name: "company_company_id", newName: "company_api_company_id");
            RenameColumn(table: "dbo.ApiUsers", name: "company_company_id", newName: "company_api_company_id");
        }
    }
}
