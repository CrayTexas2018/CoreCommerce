namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "provider_id");
            DropColumn("dbo.Users", "initial_url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "initial_url", c => c.String());
            AddColumn("dbo.Users", "provider_id", c => c.Int(nullable: false));
        }
    }
}
