namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class box_comment_reply : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BoxComments", "reply_id", c => c.Int());
            CreateIndex("dbo.BoxComments", "reply_id");
            AddForeignKey("dbo.BoxComments", "reply_id", "dbo.BoxComments", "comment_id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoxComments", "reply_id", "dbo.BoxComments");
            DropIndex("dbo.BoxComments", new[] { "reply_id" });
            DropColumn("dbo.BoxComments", "reply_id");
        }
    }
}
