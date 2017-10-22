namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeusernameandpasswordfromcompany : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Companies", "Username");
            DropColumn("dbo.Companies", "password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "password", c => c.String());
            AddColumn("dbo.Companies", "Username", c => c.String());
        }
    }
}
