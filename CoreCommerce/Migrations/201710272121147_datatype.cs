namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopifyImages", "variant_ids", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopifyImages", "variant_ids");
        }
    }
}
