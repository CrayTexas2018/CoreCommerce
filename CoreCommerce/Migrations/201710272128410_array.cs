namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class array : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShopifyImages", "variant_ids");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShopifyImages", "variant_ids", c => c.String());
        }
    }
}
