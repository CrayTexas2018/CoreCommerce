namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shopify_url : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "shopify_url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "shopify_url");
        }
    }
}
