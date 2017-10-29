namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyImages", "active_changed", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyImages", "created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyImages", "updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopifyImages", "updated");
            DropColumn("dbo.ShopifyImages", "created");
            DropColumn("dbo.ShopifyImages", "active_changed");
        }
    }
}
