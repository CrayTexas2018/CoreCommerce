namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUserEmailUniqueIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.Users", "email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "email" });
            AlterColumn("dbo.Users", "email", c => c.String(nullable: false));
        }
    }
}
