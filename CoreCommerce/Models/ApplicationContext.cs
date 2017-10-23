using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class ApplicationContext : DbContext
    {    
        public ApplicationContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<BoxComment> BoxComments { get; set; }
        public DbSet<BoxItem> BoxItems { get; set; }
        public DbSet<BoxRating> BoxRatings { get; set; }
        public DbSet<CompanyLogin> CompanyLogins { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemComment> ItemComments { get; set; }
        public DbSet<ItemRating> ItemRatings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        
        public DbSet<Company> Companies { get; set; }
        public DbSet<ApiUser> ApiUsers { get; set; }
    } 
}