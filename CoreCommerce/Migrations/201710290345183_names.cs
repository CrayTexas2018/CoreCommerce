namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class names : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyImages", "database_deleted", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyImages", "database_created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyImages", "database_updated", c => c.DateTime(nullable: false));
            DropColumn("dbo.ShopifyImages", "deleted");
            DropColumn("dbo.ShopifyImages", "created");
            DropColumn("dbo.ShopifyImages", "updated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopifyImages", "updated", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyImages", "created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyImages", "deleted", c => c.DateTime(nullable: false));
            DropColumn("dbo.ShopifyImages", "database_updated");
            DropColumn("dbo.ShopifyImages", "database_created");
            DropColumn("dbo.ShopifyImages", "database_deleted");
        }
    }
}
