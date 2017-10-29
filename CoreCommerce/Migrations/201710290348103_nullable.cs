namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ShopifyImages", "database_deleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShopifyImages", "database_deleted", c => c.DateTime(nullable: false));
        }
    }
}
