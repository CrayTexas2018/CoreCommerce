namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checkouts", "is_deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Checkouts", "completed", c => c.DateTime());
            AlterColumn("dbo.Checkouts", "deleted", c => c.DateTime());
            DropColumn("dbo.Checkouts", "id_deleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Checkouts", "id_deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Checkouts", "deleted", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Checkouts", "completed", c => c.DateTime(nullable: false));
            DropColumn("dbo.Checkouts", "is_deleted");
        }
    }
}
