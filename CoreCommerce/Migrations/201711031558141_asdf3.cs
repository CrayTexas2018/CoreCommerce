namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdf3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boxes", "next_box_id", c => c.Int());
            CreateIndex("dbo.Boxes", "next_box_id");
            AddForeignKey("dbo.Boxes", "next_box_id", "dbo.Boxes", "box_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boxes", "next_box_id", "dbo.Boxes");
            DropIndex("dbo.Boxes", new[] { "next_box_id" });
            DropColumn("dbo.Boxes", "next_box_id");
        }
    }
}
