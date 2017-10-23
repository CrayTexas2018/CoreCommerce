namespace CoreCommerce.Migrations
{
    using CoreCommerce.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoreCommerce.Models.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CoreCommerce.Models.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //CompanyRepository cr = new CompanyRepository(context);
            //UserRepository r = new UserRepository(context);
            //ApiUserRepository aur = new ApiUserRepository(context);

            //Company c = new Company
            //{
            //    active = true,
            //    created = DateTime.Now,
            //    logo = "http://www.tullyelite.com/images/images/TEST-logo-red.png",
            //    name = "Test Company",
            //    updated = DateTime.Now,
            //    website = "http://google.com"
            //};

            //c = cr.CreateApicompany(c);

            //ApiUser u = new ApiUser
            //{
            //    company = c,
            //    password = "password",
            //    Username = "Test"                
            //};

            //u = aur.CreateApiUser(u);
        }
    }
}
