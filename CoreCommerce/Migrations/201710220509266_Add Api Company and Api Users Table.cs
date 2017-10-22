namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApiCompanyandApiUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiCompanies",
                c => new
                    {
                        api_company_id = c.Int(nullable: false, identity: true),
                        company_name = c.String(),
                    })
                .PrimaryKey(t => t.api_company_id);
            
            CreateTable(
                "dbo.ApiUsers",
                c => new
                    {
                        api_user_id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        password = c.String(),
                        company_api_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.api_user_id)
                .ForeignKey("dbo.ApiCompanies", t => t.company_api_company_id)
                .Index(t => t.company_api_company_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApiUsers", "company_api_company_id", "dbo.ApiCompanies");
            DropIndex("dbo.ApiUsers", new[] { "company_api_company_id" });
            DropTable("dbo.ApiUsers");
            DropTable("dbo.ApiCompanies");
        }
    }
}
