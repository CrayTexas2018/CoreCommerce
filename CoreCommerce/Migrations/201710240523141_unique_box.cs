namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique_box : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BoxRatings", new[] { "box_id" });
            DropIndex("dbo.BoxRatings", new[] { "user_id" });
            CreateIndex("dbo.BoxRatings", "box_id", unique: true);
            CreateIndex("dbo.BoxRatings", "user_id", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.BoxRatings", new[] { "user_id" });
            DropIndex("dbo.BoxRatings", new[] { "box_id" });
            CreateIndex("dbo.BoxRatings", "user_id");
            CreateIndex("dbo.BoxRatings", "box_id");
        }
    }
}
