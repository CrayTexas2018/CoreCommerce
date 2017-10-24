using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class Item
    {
        [Key]
        public int item_id { get; set; }

        [JsonIgnore]
        [Column("company_id")]
        public Company company { get; set; }

        public string item_name { get; set; }

        public decimal price { get; set; }

        public string description { get; set; }

        public string image_url { get; set; }

        public string item_url { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostItem
    {
        public string item_name { get; set; }

        public decimal price { get; set; }

        public string description { get; set; }

        public string image_url { get; set; }

        public string item_url { get; set; }

        public bool active { get; set; }
    }

    public interface IItemRepository
    {
        IEnumerable<Item> GetItems();
        IEnumerable<Item> GetCompanyItems(int company_id);
        Item GetItem(int item_id);
        Item CreateItem(PostItem item);
        void UpdateItem(Item item);
        void DeleteItem(int item_id);
        void Save();
    }

    public class ItemRepository : IItemRepository
    {
        private ApplicationContext context;

        public ItemRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Item CreateItem(PostItem postItem)
        {
            CompanyRepository cr = new CompanyRepository(context);

            Item item = new Item();
            item.active = postItem.active;
            item.item_url = postItem.item_url;
            item.price = postItem.price;
            item.created = DateTime.Now;
            item.updated = DateTime.Now;
            item.company = cr.GetCompanyFromApiUser();

            context.Items.Add(item);
            Save();

            return item;
        }

        public void DeleteItem(int item_id)
        {
            context.Items.Remove(context.Items.Find(item_id));
        }

        public IEnumerable<Item> GetCompanyItems(int company_id)
        {
            return context.Items.Where(x => x.company.company_id == company_id).ToList();
        }

        public Item GetItem(int item_id)
        {
            return context.Items.Find(item_id);
        }

        public IEnumerable<Item> GetItems()
        {
            return context.Items.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            item.updated = DateTime.Now;

            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}