namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boolname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyImages", "is_deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopifyImages", "is_deleted");
        }
    }
}
