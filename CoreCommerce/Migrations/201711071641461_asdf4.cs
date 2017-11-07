namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "mail_gun_key", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "mail_gun_key");
        }
    }
}
