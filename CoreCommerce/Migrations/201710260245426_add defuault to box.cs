namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddefuaulttobox : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies");
            DropIndex("dbo.Boxes", new[] { "company_company_id" });
            RenameColumn(table: "dbo.Boxes", name: "company_company_id", newName: "company_id");
            AlterColumn("dbo.Boxes", "company_id", c => c.Int(nullable: false));
            CreateIndex("dbo.Boxes", "company_id");
            AddForeignKey("dbo.Boxes", "company_id", "dbo.Companies", "company_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boxes", "company_id", "dbo.Companies");
            DropIndex("dbo.Boxes", new[] { "company_id" });
            AlterColumn("dbo.Boxes", "company_id", c => c.Int());
            RenameColumn(table: "dbo.Boxes", name: "company_id", newName: "company_company_id");
            CreateIndex("dbo.Boxes", "company_company_id");
            AddForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies", "company_id");
        }
    }
}
