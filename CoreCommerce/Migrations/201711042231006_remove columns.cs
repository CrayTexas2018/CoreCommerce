namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecolumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.StripeEvents", "json");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StripeEvents", "json", c => c.String());
        }
    }
}
