namespace CoreCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoxComments",
                c => new
                    {
                        comment_id = c.Int(nullable: false, identity: true),
                        comment = c.String(),
                        upvotes = c.Int(nullable: false),
                        downvotes = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        box_box_id = c.Int(),
                        user_user_id = c.Int(),
                    })
                .PrimaryKey(t => t.comment_id)
                .ForeignKey("dbo.Boxes", t => t.box_box_id)
                .ForeignKey("dbo.Users", t => t.user_user_id)
                .Index(t => t.box_box_id)
                .Index(t => t.user_user_id);
            
            CreateTable(
                "dbo.Boxes",
                c => new
                    {
                        box_id = c.Int(nullable: false, identity: true),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.box_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        email = c.String(nullable: false),
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
                    })
                .PrimaryKey(t => t.user_id);
            
            CreateTable(
                "dbo.BoxItems",
                c => new
                    {
                        box_item_id = c.Int(nullable: false, identity: true),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        box_box_id = c.Int(),
                        item_item_id = c.Int(),
                    })
                .PrimaryKey(t => t.box_item_id)
                .ForeignKey("dbo.Boxes", t => t.box_box_id)
                .ForeignKey("dbo.Items", t => t.item_item_id)
                .Index(t => t.box_box_id)
                .Index(t => t.item_item_id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        item_id = c.Int(nullable: false, identity: true),
                        item_name = c.String(),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        description = c.String(),
                        image_url = c.String(),
                        company_url = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.item_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        company_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        website = c.String(),
                        logo = c.String(),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.company_id);
            
            CreateTable(
                "dbo.BoxRatings",
                c => new
                    {
                        box_rating_id = c.Int(nullable: false, identity: true),
                        rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        box_box_id = c.Int(),
                    })
                .PrimaryKey(t => t.box_rating_id)
                .ForeignKey("dbo.Boxes", t => t.box_box_id)
                .Index(t => t.box_box_id);
            
            CreateTable(
                "dbo.CompanyLogins",
                c => new
                    {
                        company_login_id = c.Int(nullable: false, identity: true),
                        login_date = c.DateTime(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_user_company_user_id = c.Int(),
                    })
                .PrimaryKey(t => t.company_login_id)
                .ForeignKey("dbo.CompanyUsers", t => t.company_user_company_user_id)
                .Index(t => t.company_user_company_user_id);
            
            CreateTable(
                "dbo.CompanyUsers",
                c => new
                    {
                        company_user_id = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        password = c.String(),
                        admin = c.Boolean(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        company_company_id = c.Int(),
                    })
                .PrimaryKey(t => t.company_user_id)
                .ForeignKey("dbo.Companies", t => t.company_company_id)
                .Index(t => t.company_company_id);
            
            CreateTable(
                "dbo.ItemComments",
                c => new
                    {
                        comment_id = c.Int(nullable: false, identity: true),
                        comment = c.String(),
                        upvotes = c.Int(nullable: false),
                        downvotes = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        item_item_id = c.Int(),
                        user_user_id = c.Int(),
                    })
                .PrimaryKey(t => t.comment_id)
                .ForeignKey("dbo.Items", t => t.item_item_id)
                .ForeignKey("dbo.Users", t => t.user_user_id)
                .Index(t => t.item_item_id)
                .Index(t => t.user_user_id);
            
            CreateTable(
                "dbo.ItemRatings",
                c => new
                    {
                        item_rating_id = c.Int(nullable: false, identity: true),
                        rating = c.Int(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        item_item_id = c.Int(),
                        user_user_id = c.Int(),
                    })
                .PrimaryKey(t => t.item_rating_id)
                .ForeignKey("dbo.Items", t => t.item_item_id)
                .ForeignKey("dbo.Users", t => t.user_user_id)
                .Index(t => t.item_item_id)
                .Index(t => t.user_user_id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        order_id = c.Int(nullable: false, identity: true),
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
                        subscription_subscription_id = c.Int(),
                        user_user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.order_id)
                .ForeignKey("dbo.Subscriptions", t => t.subscription_subscription_id)
                .ForeignKey("dbo.Users", t => t.user_user_id, cascadeDelete: true)
                .Index(t => t.subscription_subscription_id)
                .Index(t => t.user_user_id);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        subscription_id = c.Int(nullable: false, identity: true),
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
                        user_user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.subscription_id)
                .ForeignKey("dbo.Users", t => t.user_user_id, cascadeDelete: true)
                .Index(t => t.user_user_id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        login_id = c.Int(nullable: false, identity: true),
                        ip_address = c.String(),
                        url = c.String(),
                        login_date = c.DateTime(nullable: false),
                        active = c.Boolean(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(nullable: false),
                        user_user_id = c.Int(),
                    })
                .PrimaryKey(t => t.login_id)
                .ForeignKey("dbo.Users", t => t.user_user_id)
                .Index(t => t.user_user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLogins", "user_user_id", "dbo.Users");
            DropForeignKey("dbo.Orders", "user_user_id", "dbo.Users");
            DropForeignKey("dbo.Orders", "subscription_subscription_id", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "user_user_id", "dbo.Users");
            DropForeignKey("dbo.ItemRatings", "user_user_id", "dbo.Users");
            DropForeignKey("dbo.ItemRatings", "item_item_id", "dbo.Items");
            DropForeignKey("dbo.ItemComments", "user_user_id", "dbo.Users");
            DropForeignKey("dbo.ItemComments", "item_item_id", "dbo.Items");
            DropForeignKey("dbo.CompanyLogins", "company_user_company_user_id", "dbo.CompanyUsers");
            DropForeignKey("dbo.CompanyUsers", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxRatings", "box_box_id", "dbo.Boxes");
            DropForeignKey("dbo.BoxItems", "item_item_id", "dbo.Items");
            DropForeignKey("dbo.Items", "company_company_id", "dbo.Companies");
            DropForeignKey("dbo.BoxItems", "box_box_id", "dbo.Boxes");
            DropForeignKey("dbo.BoxComments", "user_user_id", "dbo.Users");
            DropForeignKey("dbo.BoxComments", "box_box_id", "dbo.Boxes");
            DropIndex("dbo.UserLogins", new[] { "user_user_id" });
            DropIndex("dbo.Subscriptions", new[] { "user_user_id" });
            DropIndex("dbo.Orders", new[] { "user_user_id" });
            DropIndex("dbo.Orders", new[] { "subscription_subscription_id" });
            DropIndex("dbo.ItemRatings", new[] { "user_user_id" });
            DropIndex("dbo.ItemRatings", new[] { "item_item_id" });
            DropIndex("dbo.ItemComments", new[] { "user_user_id" });
            DropIndex("dbo.ItemComments", new[] { "item_item_id" });
            DropIndex("dbo.CompanyUsers", new[] { "company_company_id" });
            DropIndex("dbo.CompanyLogins", new[] { "company_user_company_user_id" });
            DropIndex("dbo.BoxRatings", new[] { "box_box_id" });
            DropIndex("dbo.Items", new[] { "company_company_id" });
            DropIndex("dbo.BoxItems", new[] { "item_item_id" });
            DropIndex("dbo.BoxItems", new[] { "box_box_id" });
            DropIndex("dbo.BoxComments", new[] { "user_user_id" });
            DropIndex("dbo.BoxComments", new[] { "box_box_id" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Orders");
            DropTable("dbo.ItemRatings");
            DropTable("dbo.ItemComments");
            DropTable("dbo.CompanyUsers");
            DropTable("dbo.CompanyLogins");
            DropTable("dbo.BoxRatings");
            DropTable("dbo.Companies");
            DropTable("dbo.Items");
            DropTable("dbo.BoxItems");
            DropTable("dbo.Users");
            DropTable("dbo.Boxes");
            DropTable("dbo.BoxComments");
        }
    }
}
