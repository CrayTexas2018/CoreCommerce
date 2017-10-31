namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkoutfk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Checkouts", "user_id");
            AddForeignKey("dbo.Checkouts", "user_id", "dbo.Users", "user_id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checkouts", "user_id", "dbo.Users");
            DropIndex("dbo.Checkouts", new[] { "user_id" });
        }
    }
}
