namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApiCompanies", "active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApiCompanies", "created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApiCompanies", "updated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "company_api_company_id", c => c.Int());
            CreateIndex("dbo.Users", "company_api_company_id");
            AddForeignKey("dbo.Users", "company_api_company_id", "dbo.ApiCompanies", "api_company_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "company_api_company_id", "dbo.ApiCompanies");
            DropIndex("dbo.Users", new[] { "company_api_company_id" });
            DropColumn("dbo.Users", "company_api_company_id");
            DropColumn("dbo.ApiCompanies", "updated");
            DropColumn("dbo.ApiCompanies", "created");
            DropColumn("dbo.ApiCompanies", "active");
        }
    }
}
