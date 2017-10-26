namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Common_Fields_All_Classes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.Items", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.CompanyUsers", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.Orders", "company_company_id", "dbo.Companies");
            DropIndex("dbo.Users", new[] { "company_company_id" });
            DropIndex("dbo.Items", new[] { "company_company_id" });
            DropIndex("dbo.CompanyUsers", new[] { "company_company_id" });
            DropIndex("dbo.Orders", new[] { "company_company_id" });
            RenameColumn(table: "dbo.Users", name: "company_company_id", newName: "company_id");
            RenameColumn(table: "dbo.Items", name: "company_company_id", newName: "company_id");
            RenameColumn(table: "dbo.CompanyUsers", name: "company_company_id", newName: "company_id");
            RenameColumn(table: "dbo.Orders", name: "company_company_id", newName: "company_id");
            AddColumn("dbo.BoxComments", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.BoxCommentVotes", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.BoxItems", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.BoxRatings", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.CompanyLogins", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.ItemComments", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.ItemRatings", "company_id", c => c.Int(nullable: false));
            AddColumn("dbo.UserLogins", "company_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "company_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "company_id", c => c.Int(nullable: false));
            AlterColumn("dbo.CompanyUsers", "company_id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "company_id", c => c.Int(nullable: false));
            CreateIndex("dbo.BoxComments", "company_id");
            CreateIndex("dbo.Users", "company_id");
            CreateIndex("dbo.BoxCommentVotes", "company_id");
            CreateIndex("dbo.BoxItems", "company_id");
            CreateIndex("dbo.Items", "company_id");
            CreateIndex("dbo.BoxRatings", "company_id");
            CreateIndex("dbo.CompanyLogins", "company_id");
            CreateIndex("dbo.CompanyUsers", "company_id");
            CreateIndex("dbo.ItemComments", "company_id");
            CreateIndex("dbo.ItemRatings", "company_id");
            CreateIndex("dbo.Orders", "company_id");
            CreateIndex("dbo.UserLogins", "company_id");
            AddForeignKey("dbo.BoxComments", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.BoxCommentVotes", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.BoxItems", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.BoxRatings", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.CompanyLogins", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.ItemComments", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.ItemRatings", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.UserLogins", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.Users", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.Items", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.CompanyUsers", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
            AddForeignKey("dbo.Orders", "company_id", "dbo.Companies", "company_id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "company_id", "dbo.Companies");
            DropForeignKey("dbo.CompanyUsers", "company_id", "dbo.Companies");
            DropForeignKey("dbo.Items", "company_id", "dbo.Companies");
            DropForeignKey("dbo.Users", "company_id", "dbo.Companies");
            DropForeignKey("dbo.UserLogins", "company_id", "dbo.Companies");
            DropForeignKey("dbo.ItemRatings", "company_id", "dbo.Companies");
            DropForeignKey("dbo.ItemComments", "company_id", "dbo.Companies");
            DropForeignKey("dbo.CompanyLogins", "company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxRatings", "company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxItems", "company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxCommentVotes", "company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxComments", "company_id", "dbo.Companies");
            DropIndex("dbo.UserLogins", new[] { "company_id" });
            DropIndex("dbo.Orders", new[] { "company_id" });
            DropIndex("dbo.ItemRatings", new[] { "company_id" });
            DropIndex("dbo.ItemComments", new[] { "company_id" });
            DropIndex("dbo.CompanyUsers", new[] { "company_id" });
            DropIndex("dbo.CompanyLogins", new[] { "company_id" });
            DropIndex("dbo.BoxRatings", new[] { "company_id" });
            DropIndex("dbo.Items", new[] { "company_id" });
            DropIndex("dbo.BoxItems", new[] { "company_id" });
            DropIndex("dbo.BoxCommentVotes", new[] { "company_id" });
            DropIndex("dbo.Users", new[] { "company_id" });
            DropIndex("dbo.BoxComments", new[] { "company_id" });
            AlterColumn("dbo.Orders", "company_id", c => c.Int());
            AlterColumn("dbo.CompanyUsers", "company_id", c => c.Int());
            AlterColumn("dbo.Items", "company_id", c => c.Int());
            AlterColumn("dbo.Users", "company_id", c => c.Int());
            DropColumn("dbo.UserLogins", "company_id");
            DropColumn("dbo.ItemRatings", "company_id");
            DropColumn("dbo.ItemComments", "company_id");
            DropColumn("dbo.CompanyLogins", "company_id");
            DropColumn("dbo.BoxRatings", "company_id");
            DropColumn("dbo.BoxItems", "company_id");
            DropColumn("dbo.BoxCommentVotes", "company_id");
            DropColumn("dbo.BoxComments", "company_id");
            RenameColumn(table: "dbo.Orders", name: "company_id", newName: "company_company_id");
            RenameColumn(table: "dbo.CompanyUsers", name: "company_id", newName: "company_company_id");
            RenameColumn(table: "dbo.Items", name: "company_id", newName: "company_company_id");
            RenameColumn(table: "dbo.Users", name: "company_id", newName: "company_company_id");
            CreateIndex("dbo.Orders", "company_company_id");
            CreateIndex("dbo.CompanyUsers", "company_company_id");
            CreateIndex("dbo.Items", "company_company_id");
            CreateIndex("dbo.Users", "company_company_id");
            AddForeignKey("dbo.Orders", "company_company_id", "dbo.Companies", "company_id");
            AddForeignKey("dbo.CompanyUsers", "company_company_id", "dbo.Companies", "company_id");
            AddForeignKey("dbo.Items", "company_company_id", "dbo.Companies", "company_id");
            AddForeignKey("dbo.Users", "company_company_id", "dbo.Companies", "company_id");
        }
    }
}
