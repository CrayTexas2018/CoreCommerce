namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatevariants : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyVariants", "is_deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ShopifyVariants", "database_deleted", c => c.DateTime());
            AddColumn("dbo.ShopifyVariants", "database_created", c => c.DateTime(nullable: false));
            AddColumn("dbo.ShopifyVariants", "database_updated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopifyVariants", "database_updated");
            DropColumn("dbo.ShopifyVariants", "database_created");
            DropColumn("dbo.ShopifyVariants", "database_deleted");
            DropColumn("dbo.ShopifyVariants", "is_deleted");
        }
    }
}
