namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addscoreremoveupanddownvote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoxComments", "score", c => c.Int(nullable: false));
            DropColumn("dbo.BoxComments", "upvotes");
            DropColumn("dbo.BoxComments", "downvotes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BoxComments", "downvotes", c => c.Int(nullable: false));
            AddColumn("dbo.BoxComments", "upvotes", c => c.Int(nullable: false));
            DropColumn("dbo.BoxComments", "score");
        }
    }
}
