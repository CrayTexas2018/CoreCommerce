namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class company_shopify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "shopify_secret", c => c.String());
            AddColumn("dbo.Companies", "shopify_password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "shopify_password");
            DropColumn("dbo.Companies", "shopify_secret");
        }
    }
}
