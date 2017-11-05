namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stripe_event : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StripeEvents",
                c => new
                    {
                        event_id = c.String(nullable: false, maxLength: 128),
                        stripe_object = c.String(),
                        json = c.String(),
                        created = c.DateTime(),
                    })
                .PrimaryKey(t => t.event_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StripeEvents");
        }
    }
}
