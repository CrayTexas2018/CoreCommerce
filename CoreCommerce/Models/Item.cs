using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class Item : CommonFields
    {
        [Key]
        public int item_id { get; set; }

        [Index(IsUnique = true, Order = 1)]
        [MaxLength(255)]
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
            Item item = new Item();
            item.active = postItem.active;
            item.item_url = postItem.item_url;
            item.price = postItem.price;
            item.created = DateTime.Now;
            item.updated = DateTime.Now;

            context.Items.Add(item);
            Save();

            return item;
        }

        public void DeleteItem(int item_id)
        {
            // verify item is valid
            Item item = GetItem(item_id);
            context.Items.Remove(item);
        }

        public IEnumerable<Item> GetCompanyItems()
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.Items.Where(x => x.company.company_id == company_id).ToList();
        }

        public Item GetItem(int item_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            Item item = context.Items.Where(x => x.item_id == item_id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (item != null)
            {
                return item;
            }
            throw new Exception("Item ID " + item.item_id + " not found.");
        }

        public IEnumerable<Item> GetItems()
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            return context.Items.Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            Item verify = GetItem(item.item_id);

            if (verify != null)
            {
                item.updated = DateTime.Now;

                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
        }
    }
}