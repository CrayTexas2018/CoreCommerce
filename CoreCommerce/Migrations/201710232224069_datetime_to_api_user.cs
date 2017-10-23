namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetime_to_api_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApiUsers", "created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ApiUsers", "updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApiUsers", "updated");
            DropColumn("dbo.ApiUsers", "created");
        }
    }
}
