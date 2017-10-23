namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adduniquetoemailandcompanyname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "name", c => c.String(maxLength: 255));
            AlterColumn("dbo.ApiUsers", "Username", c => c.String(maxLength: 255));
            AlterColumn("dbo.CompanyUsers", "email", c => c.String(maxLength: 255));
            CreateIndex("dbo.ApiUsers", "Username", unique: true);
            CreateIndex("dbo.Companies", "name", unique: true);
            CreateIndex("dbo.CompanyUsers", "email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.CompanyUsers", new[] { "email" });
            DropIndex("dbo.Companies", new[] { "name" });
            DropIndex("dbo.ApiUsers", new[] { "Username" });
            AlterColumn("dbo.CompanyUsers", "email", c => c.String());
            AlterColumn("dbo.ApiUsers", "Username", c => c.String());
            AlterColumn("dbo.Companies", "name", c => c.String());
        }
    }
}
