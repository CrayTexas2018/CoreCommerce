namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiUsers",
                c => new
                    {
                        api_user_id = c.Int(nullable: false, identity: true),
                        Username = c.String(maxLength: 255),
                        password = c.String(),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.api_user_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.Username, unique: true)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        company_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        website = c.String(),
                        logo = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.company_id)
                .Index(t => t.name, unique: true);
            
            CreateTable(
                "dbo.BoxComments",
                c => new
                    {
                        comment_id = c.Int(nullable: false, identity: true),
                        box_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        comment = c.String(),
                        reply_id = c.Int(),
                        score = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.comment_id)
                .ForeignKey("dbo.Boxes", t => t.box_id, cascadeDelete: true)
                .ForeignKey("dbo.BoxComments", t => t.reply_id)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.box_id)
                .Index(t => t.user_id)
                .Index(t => t.reply_id);
            
            CreateTable(
                "dbo.Boxes",
                c => new
                    {
                        box_id = c.Int(nullable: false, identity: true),
                        box_name = c.String(maxLength: 255),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.box_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.box_name, unique: true)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false, maxLength: 255),
                        password = c.String(nullable: false),
                        first_name = c.String(),
                        last_name = c.String(),
                        address_1 = c.String(),
                        address_2 = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zip = c.Int(nullable: false),
                        provider_id = c.Int(nullable: false),
                        initial_url = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.user_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.email, unique: true)
                .Index(t => t.company_company_id);
            
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
            
            CreateTable(
                "dbo.BoxItems",
                c => new
                    {
                        box_item_id = c.Int(nullable: false, identity: true),
                        box_id = c.Int(nullable: false),
                        item_id = c.Int(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.box_item_id)
                .ForeignKey("dbo.Boxes", t => t.box_id, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .Index(t => t.box_id, unique: true)
                .Index(t => t.item_id, unique: true);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        item_id = c.Int(nullable: false, identity: true),
                        item_name = c.String(maxLength: 255),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        description = c.String(),
                        image_url = c.String(),
                        item_url = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.item_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.item_name, unique: true)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.BoxRatings",
                c => new
                    {
                        box_rating_id = c.Int(nullable: false, identity: true),
                        box_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.box_rating_id)
                .ForeignKey("dbo.Boxes", t => t.box_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.box_id, unique: true)
                .Index(t => t.user_id, unique: true);
            
            CreateTable(
                "dbo.CompanyLogins",
                c => new
                    {
                        company_login_id = c.Int(nullable: false, identity: true),
                        company_user_id = c.Int(nullable: false),
                        login_date = c.DateTime(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.company_login_id)
                .ForeignKey("dbo.CompanyUsers", t => t.company_user_id, cascadeDelete: true)
                .Index(t => t.company_user_id);
            
            CreateTable(
                "dbo.CompanyUsers",
                c => new
                    {
                        company_user_id = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 255),
                        password = c.String(),
                        admin = c.Boolean(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.company_user_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.email, unique: true)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.ItemComments",
                c => new
                    {
                        comment_id = c.Int(nullable: false, identity: true),
                        item_id = c.Int(nullable: false),
                        user_id = c.Int(nullable: false),
                        comment = c.String(),
                        upvotes = c.Int(nullable: false),
                        downvotes = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.comment_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.item_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.ItemRatings",
                c => new
                    {
                        item_rating_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        item_id = c.Int(nullable: false),
                        rating = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.item_rating_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id, unique: true)
                .Index(t => t.item_id, unique: true);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        subscription_id = c.Int(),
                        first_name = c.String(nullable: false),
                        last_name = c.String(nullable: false),
                        address_1 = c.String(nullable: false),
                        address_2 = c.String(nullable: false),
                        city = c.String(nullable: false),
                        state = c.String(nullable: false),
                        zip = c.Int(nullable: false),
                        billing_address_1 = c.String(nullable: false),
                        billing_address_2 = c.String(nullable: false),
                        billing_city = c.String(nullable: false),
                        billing_state = c.String(nullable: false),
                        billing_zip = c.Int(nullable: false),
                        provider_id = c.Int(nullable: false),
                        initial_url = c.String(),
                        rebill = c.Boolean(nullable: false),
                        response = c.String(),
                        success = c.Boolean(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .ForeignKey("dbo.Subscriptions", t => t.subscription_id)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.subscription_id)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        subscription_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        first_name = c.String(),
                        last_name = c.String(),
                        address_1 = c.String(),
                        address_2 = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zip = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.subscription_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        login_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        ip_address = c.String(),
                        url = c.String(),
                        login_date = c.DateTime(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.login_id)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLogins", "user_id", "dbo.Users");
            DropForeignKey("dbo.Orders", "user_id", "dbo.Users");
            DropForeignKey("dbo.Orders", "subscription_id", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "user_id", "dbo.Users");
            DropForeignKey("dbo.Subscriptions", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.Orders", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.ItemRatings", "user_id", "dbo.Users");
            DropForeignKey("dbo.ItemRatings", "item_id", "dbo.Items");
            DropForeignKey("dbo.ItemComments", "user_id", "dbo.Users");
            DropForeignKey("dbo.ItemComments", "item_id", "dbo.Items");
            DropForeignKey("dbo.CompanyLogins", "company_user_id", "dbo.CompanyUsers");
            DropForeignKey("dbo.CompanyUsers", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxRatings", "user_id", "dbo.Users");
            DropForeignKey("dbo.BoxRatings", "box_id", "dbo.Boxes");
            DropForeignKey("dbo.BoxItems", "item_id", "dbo.Items");
            DropForeignKey("dbo.Items", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxItems", "box_id", "dbo.Boxes");
            DropForeignKey("dbo.BoxCommentVotes", "user_id", "dbo.Users");
            DropForeignKey("dbo.BoxCommentVotes", "box_comment_id", "dbo.BoxComments");
            DropForeignKey("dbo.BoxComments", "user_id", "dbo.Users");
            DropForeignKey("dbo.Users", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxComments", "reply_id", "dbo.BoxComments");
            DropForeignKey("dbo.BoxComments", "box_id", "dbo.Boxes");
            DropForeignKey("dbo.Boxes", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.ApiUsers", "company_company_id", "dbo.Companies");
            DropIndex("dbo.UserLogins", new[] { "user_id" });
            DropIndex("dbo.Subscriptions", new[] { "company_company_id" });
            DropIndex("dbo.Subscriptions", new[] { "user_id" });
            DropIndex("dbo.Orders", new[] { "company_company_id" });
            DropIndex("dbo.Orders", new[] { "subscription_id" });
            DropIndex("dbo.Orders", new[] { "user_id" });
            DropIndex("dbo.ItemRatings", new[] { "item_id" });
            DropIndex("dbo.ItemRatings", new[] { "user_id" });
            DropIndex("dbo.ItemComments", new[] { "user_id" });
            DropIndex("dbo.ItemComments", new[] { "item_id" });
            DropIndex("dbo.CompanyUsers", new[] { "company_company_id" });
            DropIndex("dbo.CompanyUsers", new[] { "email" });
            DropIndex("dbo.CompanyLogins", new[] { "company_user_id" });
            DropIndex("dbo.BoxRatings", new[] { "user_id" });
            DropIndex("dbo.BoxRatings", new[] { "box_id" });
            DropIndex("dbo.Items", new[] { "company_company_id" });
            DropIndex("dbo.Items", new[] { "item_name" });
            DropIndex("dbo.BoxItems", new[] { "item_id" });
            DropIndex("dbo.BoxItems", new[] { "box_id" });
            DropIndex("dbo.BoxCommentVotes", new[] { "box_comment_id" });
            DropIndex("dbo.BoxCommentVotes", new[] { "user_id" });
            DropIndex("dbo.Users", new[] { "company_company_id" });
            DropIndex("dbo.Users", new[] { "email" });
            DropIndex("dbo.Boxes", new[] { "company_company_id" });
            DropIndex("dbo.Boxes", new[] { "box_name" });
            DropIndex("dbo.BoxComments", new[] { "reply_id" });
            DropIndex("dbo.BoxComments", new[] { "user_id" });
            DropIndex("dbo.BoxComments", new[] { "box_id" });
            DropIndex("dbo.Companies", new[] { "name" });
            DropIndex("dbo.ApiUsers", new[] { "company_company_id" });
            DropIndex("dbo.ApiUsers", new[] { "Username" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Orders");
            DropTable("dbo.ItemRatings");
            DropTable("dbo.ItemComments");
            DropTable("dbo.CompanyUsers");
            DropTable("dbo.CompanyLogins");
            DropTable("dbo.BoxRatings");
            DropTable("dbo.Items");
            DropTable("dbo.BoxItems");
            DropTable("dbo.BoxCommentVotes");
            DropTable("dbo.Users");
            DropTable("dbo.Boxes");
            DropTable("dbo.BoxComments");
            DropTable("dbo.Companies");
            DropTable("dbo.ApiUsers");
        }
    }
}
