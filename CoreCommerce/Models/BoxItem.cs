using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxItem : CommonFields
    {
        [Key]
        [JsonIgnore]
        public int box_item_id { get; set; }

        [Index(IsUnique = true, Order = 0)]
        public int box_id { get; set; }

        [ForeignKey("box_id")]
        [JsonIgnore]
        [Column("box_id")]
        public Box box { get; set; }

        [Index(IsUnique = true, Order = 1)]
        public int item_id { get; set; }

        [ForeignKey("item_id")]
        [JsonIgnore]
        [Column("item_id")]        
        public Item item { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostBoxItem
    {
        [ForeignKey("box_id")]
        public int box_id { get; set; }

        [ForeignKey("item_id")]
        public int item_id { get; set; }
    }

    public interface IBoxItemRepository
    {
        IEnumerable<BoxItem> GetBoxItems(int box_id);
        BoxItem GetBoxItem(int item_id);
        BoxItem CreateBoxItem(PostBoxItem item);
        void UpdateBoxItem(BoxItem item);
        void DeleteBoxItem(int item_id);
        void Save();
    }

    public class BoxItemRepository : IBoxItemRepository
    {
        private ApplicationContext context;

        public BoxItemRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public BoxItem CreateBoxItem(PostBoxItem postItem)
        {
            ItemRepository ir = new ItemRepository(context);
            BoxRepository br = new BoxRepository(context);
            CompanyRepository cr = new CompanyRepository(context);

            BoxItem item = new BoxItem
            {
                box_id = postItem.box_id,
                company_id = cr.GetCompanyIdFromApiUser(),
                item_id = postItem.item_id,
                created = DateTime.Now,
                updated = DateTime.Now
            };

            context.BoxItems.Add(item);
            Save();

            return item;
        }

        public void DeleteBoxItem(int item_id)
        {
            // make sure product isnt null for company
            BoxItem item = GetBoxItem(item_id);
            context.BoxItems.Remove(item);
            Save();
        }

        public BoxItem GetBoxItem(int item_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            BoxItem item = context.BoxItems.Where(x => x.item_id == item_id).Where(x => x.company_id == company_id).FirstOrDefault();

            if (item != null)
            {
                return item;
            }
            throw new Exception("Box item ID " + item.item_id + " not found.");
        }

        public IEnumerable<BoxItem> GetBoxItems(int box_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.BoxItems.Where(x => x.box.box_id == box_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxItem(BoxItem item)
        {
            // Make sure item belongs to company
            BoxItem verify = GetBoxItem(item.item_id);
            item.updated = DateTime.Now;
            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}