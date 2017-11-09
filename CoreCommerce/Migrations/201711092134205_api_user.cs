namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class api_user : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        email_id = c.Int(nullable: false, identity: true),
                        email_name = c.String(),
                        email_template = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.email_id)
                .ForeignKey("dbo.Companies", t => t.company_id, cascadeDelete: true)
                .Index(t => t.company_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmailTemplates", "company_id", "dbo.Companies");
            DropIndex("dbo.EmailTemplates", new[] { "company_id" });
            DropTable("dbo.EmailTemplates");
        }
    }
}
