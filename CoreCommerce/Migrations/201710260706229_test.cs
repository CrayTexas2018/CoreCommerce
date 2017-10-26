namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApiUsers", "company_company_id", "dbo.Companies");
            DropIndex("dbo.ApiUsers", new[] { "company_company_id" });
            RenameColumn(table: "dbo.ApiUsers", name: "company_company_id", newName: "company_id");
            AlterColumn("dbo.ApiUsers", "company_id", c => c.Int(nullable: false));
            CreateIndex("dbo.ApiUsers", "company_id");
            AddForeignKey("dbo.ApiUsers", "company_id", "dbo.Companies", "company_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApiUsers", "company_id", "dbo.Companies");
            DropIndex("dbo.ApiUsers", new[] { "company_id" });
            AlterColumn("dbo.ApiUsers", "company_id", c => c.Int());
            RenameColumn(table: "dbo.ApiUsers", name: "company_id", newName: "company_company_id");
            CreateIndex("dbo.ApiUsers", "company_company_id");
            AddForeignKey("dbo.ApiUsers", "company_company_id", "dbo.Companies", "company_id");
        }
    }
}
