namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.StripeEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StripeEvents",
                c => new
                    {
                        event_id = c.String(nullable: false, maxLength: 128),
                        stripe_object = c.String(),
                        created = c.DateTime(),
                    })
                .PrimaryKey(t => t.event_id);
            
        }
    }
}
