namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "item_name", c => c.String(maxLength: 255));
            CreateIndex("dbo.Items", "item_name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "item_name" });
            AlterColumn("dbo.Items", "item_name", c => c.String());
        }
    }
}
