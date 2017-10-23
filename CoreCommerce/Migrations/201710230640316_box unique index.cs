namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class boxuniqueindex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boxes", "box_name", c => c.String(maxLength: 255));
            CreateIndex("dbo.Boxes", "box_name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Boxes", new[] { "box_name" });
            DropColumn("dbo.Boxes", "box_name");
        }
    }
}
