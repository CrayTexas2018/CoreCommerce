namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyImages", "deleted", c => c.DateTime(nullable: false));
            DropColumn("dbo.ShopifyImages", "active_changed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopifyImages", "active_changed", c => c.DateTime(nullable: false));
            DropColumn("dbo.ShopifyImages", "deleted");
        }
    }
}
