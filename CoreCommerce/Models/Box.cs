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
        
        public long shopify_product_id { get; set; }

        [JsonIgnore]
        [ForeignKey("shopify_product_id")]
        public Product shopify_product { get; set; }

        public long? shopify_variant_id { get; set; }

        [JsonIgnore]
        [ForeignKey("shopify_variant_id")]
        public Variant shopify_variant { get; set; }

        public string stipe_plan_id { get; set; }

        public int? next_box_id { get; set; }

        [ForeignKey("next_box_id")]
        [JsonIgnore]
        public Box next_box { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostBox
    {
        [MaxLength(255)]
        public string box_name { get; set; }

        public int next_box_id { get; set; }

        public long shopify_product_id { get; set; }

        public long? shopify_variant_id { get; set; }

        public string stripe_plan_id { get; set; }
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
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            Box box = new Box
            {
                box_name = postbox.box_name,
                shopify_product_id = postbox.shopify_product_id,
                shopify_variant_id = postbox.shopify_variant_id,      
                active = true,
                company_id = company_id,
                next_box_id = postbox.next_box_id,
                next_box = GetBox(postbox.next_box_id),
                created = DateTime.Now,
                updated = DateTime.Now,
                stipe_plan_id = postbox.stripe_plan_id
            };

            context.Boxes.Add(box);
            Save();
            return box;
        }

        public void DeleteBox(int box_id)
        {
            // see if box_id exists for company
            Box box = GetBox(box_id);
            if (box != null)
            {
                context.Boxes.Remove(box);
                Save();
            }
        }

        public Box GetBox(int box_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            Box box = context.Boxes.Where(x => x.box_id == box_id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (box != null)
            {
                return box;
            }
            throw new Exception("Box ID " + box.box_id + " does not exist");
        }

        public IEnumerable<Box> GetBoxes()
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            return context.Boxes.Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBox(Box box)
        {
            // Make sure box is valid for company
            Box verify = GetBox(box.box_id);

            // Error should already be thrown if not exists
            if (verify != null)
            {
                box.updated = DateTime.Now;
                context.Entry(box).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
        }
    }
}