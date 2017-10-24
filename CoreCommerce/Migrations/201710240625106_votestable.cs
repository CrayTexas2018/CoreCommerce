namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class votestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoxCommentVotes",
                c => new
                    {
                        box_comment_vote_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        box_comment_id = c.Int(nullable: false),
                        is_upvote = c.Boolean(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.box_comment_vote_id)
                .ForeignKey("dbo.BoxComments", t => t.box_comment_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: false)
                .Index(t => t.user_id)
                .Index(t => t.box_comment_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BoxCommentVotes", "user_id", "dbo.Users");
            DropForeignKey("dbo.BoxCommentVotes", "box_comment_id", "dbo.BoxComments");
            DropIndex("dbo.BoxCommentVotes", new[] { "box_comment_id" });
            DropIndex("dbo.BoxCommentVotes", new[] { "user_id" });
            DropTable("dbo.BoxCommentVotes");
        }
    }
}
