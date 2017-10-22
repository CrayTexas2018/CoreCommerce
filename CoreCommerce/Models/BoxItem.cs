using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxItem
    {
        [Key]
        public int box_item_id { get; set; }

        public Box box { get; set; }

        public Item item { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IBoxItemRepository
    {
        IEnumerable<BoxItem> GetBoxItems(int box_id);
        BoxItem CreateBoxItem(BoxItem item);
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

        public BoxItem CreateBoxItem(BoxItem item)
        {
            item.created = DateTime.Now;
            item.updated = DateTime.Now;

            context.BoxItems.Add(item);
            Save();

            return item;
        }

        public void DeleteBoxItem(int item_id)
        {
            context.BoxItems.Remove(context.BoxItems.Find(item_id));
        }

        public IEnumerable<BoxItem> GetBoxItems(int box_id)
        {
            return context.BoxItems.Where(x => x.box.box_id == box_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxItem(BoxItem item)
        {
            item.updated = DateTime.Now;

            context.Entry(item).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}