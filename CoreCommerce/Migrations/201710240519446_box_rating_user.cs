namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class box_rating_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoxRatings", "user_id", c => c.Int(nullable: false));
            CreateIndex("dbo.BoxRatings", "user_id");
            AddForeignKey("dbo.BoxRatings", "user_id", "dbo.Users", "user_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoxRatings", "user_id", "dbo.Users");
            DropIndex("dbo.BoxRatings", new[] { "user_id" });
            DropColumn("dbo.BoxRatings", "user_id");
        }
    }
}
