using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CoreCommerce.Models
{
    public class Box : CommonFields
    {
        [Key]
        public int box_id { get; set; }

        [Index(IsUnique = true, Order = 1)]
        [MaxLength(255)]
        public string box_name { get; set; }

        public int shopify_variant_id { get; set; }

        public ShopifyProduct shopify_product { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostBox
    {
        [MaxLength(255)]
        public string box_name { get; set; }
    }

    public interface IBoxRepository
    {
        IEnumerable<Box> GetBoxes();
        Box CreateBox(PostBox box);
        Box GetBox(int box_id);
        void UpdateBox(Box box);
        void DeleteBox(int box_id);
        void Save();
    }

    public class BoxRepository : IBoxRepository
    {
        private ApplicationContext context;

        public BoxRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public Box CreateBox(PostBox postbox)
        {
            Box box = new Box
            {
                box_name = postbox.box_name,
            };
            
            box.created = DateTime.Now;
            box.updated = DateTime.Now;
            box.active = true;

            context.Boxes.Add(box);
            Save();
            return box;
        }

        public void DeleteBox(int box_id)
        {
            context.Boxes.Remove(context.Boxes.Find(box_id));
        }

        public Box GetBox(int box_id)
        {
            return context.Boxes.Find(box_id);
        }

        public IEnumerable<Box> GetBoxes()
        {
            return context.Boxes.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBox(Box box)
        {
            box.updated = DateTime.Now;

            context.Entry(box).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}