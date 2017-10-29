namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyProducts", "is_deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShopifyProducts", "database_deleted", c => c.DateTime());
            AddColumn("dbo.ShopifyProducts", "database_created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyProducts", "database_updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopifyProducts", "database_updated");
            DropColumn("dbo.ShopifyProducts", "database_created");
            DropColumn("dbo.ShopifyProducts", "database_deleted");
            DropColumn("dbo.ShopifyProducts", "is_deleted");
        }
    }
}
