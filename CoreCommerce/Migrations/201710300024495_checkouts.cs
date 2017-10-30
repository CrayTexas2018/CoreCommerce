namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkouts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checkouts",
                c => new
                    {
                        checkout_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.checkout_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Checkouts");
        }
    }
}
