namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniques : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BoxItems", new[] { "box_id" });
            DropIndex("dbo.BoxItems", new[] { "item_id" });
            DropIndex("dbo.ItemRatings", new[] { "user_id" });
            DropIndex("dbo.ItemRatings", new[] { "item_id" });
            CreateIndex("dbo.BoxItems", "box_id", unique: true);
            CreateIndex("dbo.BoxItems", "item_id", unique: true);
            CreateIndex("dbo.ItemRatings", "user_id", unique: true);
            CreateIndex("dbo.ItemRatings", "item_id", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.ItemRatings", new[] { "item_id" });
            DropIndex("dbo.ItemRatings", new[] { "user_id" });
            DropIndex("dbo.BoxItems", new[] { "item_id" });
            DropIndex("dbo.BoxItems", new[] { "box_id" });
            CreateIndex("dbo.ItemRatings", "item_id");
            CreateIndex("dbo.ItemRatings", "user_id");
            CreateIndex("dbo.BoxItems", "item_id");
            CreateIndex("dbo.BoxItems", "box_id");
        }
    }
}
