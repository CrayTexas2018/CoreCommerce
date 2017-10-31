namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checkouts", "id_deleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Checkouts", "deleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Checkouts", "deleted", c => c.Boolean(nullable: false));
            DropColumn("dbo.Checkouts", "id_deleted");
        }
    }
}
