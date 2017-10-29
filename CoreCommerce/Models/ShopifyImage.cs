using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    [Table("ShopifyImages")]
    public class Image : CommonFields
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column("image_id", Order = 0)]
        public long? Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column(Order = 1)]
        public long variant_id { get; set; }

        public long product_id { get; set; }

        [ForeignKey("product_id")]
        [JsonIgnore]
        public Product prodcut { get; set; }

        public int position { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }

        public int width { get; set; }

        public int height { get; set; }

        public string src { get; set; }

        public List<Int64> variant_ids { get; set; }

        public bool is_deleted { get; set; }

        public DateTime? database_deleted { get; set; }

        public DateTime database_created { get; set; }

        public DateTime database_updated { get; set; }
    }

    public class RootShopifyImage
    {
        public List<Image> images { get; set; }
    }

    public interface IShopifyImageRepository
    {
        List<Image> GetImages();
        Image GetImage(long? image_id, long? variant_id);
        void PullImages();
        void DeleteImages();
        void RefreshImages();
        void DeleteImage(long? image_id, long? variant_id);
        void Save();
    }

    public class ShopifyImageRepository : IShopifyImageRepository
    {
        private ApplicationContext context;

        private CompanyRepository cr;

        public ShopifyImageRepository(ApplicationContext context)
        {
            this.context = context;
            cr = new CompanyRepository(context);
        }

        public void DeleteImage(long? image_id, long? variant_id)
        {
            // Do not actually delete, just set deleted to true
            Image image = GetImage(image_id, variant_id);
            image.is_deleted = true;
            image.database_deleted = DateTime.Now;
            image.database_updated = DateTime.Now;
            context.Entry(image).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteImages()
        {
            ShopifyManager manager = new ShopifyManager(context);

            ShopifyProductRepository spr = new ShopifyProductRepository(context);
            List<Product> products = spr.GetProducts();

            foreach (Product product in products)
            {
                string endpoint = "/admin/products/" + product.Id + "/images.json";
                string json = manager.shopifyGetRequest(endpoint);
                RootShopifyImage shopifyImages = JsonConvert.DeserializeObject<RootShopifyImage>(json);
                // For every imge, add or update

                // Get all images for the product in the database
                List<Image> database_images = context.ShopifyImages.Where(x => x.product_id == product.Id).ToList();

                foreach (Image database_image in database_images)
                {
                    bool delete = true;
                    foreach (Image image in shopifyImages.images)
                    {
                        int count = 0;
                        do
                        {
                            if (image.variant_ids.Count == 0)
                            {
                                image.variant_id = 0;
                            }
                            else
                            {
                                image.variant_id = image.variant_ids[count];
                            }

                            if (image.Id == database_image.Id && image.variant_id == database_image.variant_id)
                            {
                                delete = false;
                                break;
                            }
                            count++;
                        }
                        while (count < image.variant_ids.Count);
                    }
                    if (delete)
                    {
                        DeleteImage(database_image.Id, database_image.variant_id);
                    }
                }                
            }
        }

        public Image GetImage(long? image_id, long? variant_id)
        {
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.ShopifyImages.Where(x => x.company_id == company_id).Where(x => x.Id == image_id).Where(x => x.variant_id == variant_id).FirstOrDefault();
        }

        public List<Image> GetImages()
        {
            return context.ShopifyImages.Where(x => x.company_id == cr.GetCompanyIdFromApiUser()).ToList();
        }

        public void PullImages()
        {
            ShopifyManager manager = new ShopifyManager(context);

            ShopifyProductRepository spr = new ShopifyProductRepository(context);
            List<Product> products = spr.GetProducts();            

            foreach (Product product in products)
            {
                string endpoint = "/admin/products/" + product.Id +"/images.json";
                string json = manager.shopifyGetRequest(endpoint);
                RootShopifyImage shopifyImages = JsonConvert.DeserializeObject<RootShopifyImage>(json);
                // For every imge, add or update
                foreach (Image image in shopifyImages.images)
                {
                    int count = 0;
                    do
                    {
                        if (image.variant_ids.Count == 0)
                        {
                            image.variant_id = 0;
                        }
                        else
                        {
                            image.variant_id = image.variant_ids[count];
                        }

                        Image i = GetImage(image.Id, image.variant_id);
                        if (i != null)
                        {
                            DateTime dbcreate = i.database_created;
                            // existing imge, just update it
                            context.Entry(i).CurrentValues.SetValues(image);
                            i.database_updated = DateTime.Now;
                            i.database_created = dbcreate;
                            context.Entry(i).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            Image image_to_add = new Image
                            {
                                company = image.company,
                                company_id = image.company_id,
                                created_at = image.created_at,
                                database_created = image.database_created,
                                database_deleted = image.database_deleted,
                                database_updated = image.database_updated,
                                height = image.height,
                                Id =image.Id,
                                is_deleted = image.is_deleted,
                                position = image.position,
                                prodcut = image.prodcut,
                                product_id = image.product_id,
                                src = image.src,
                                updated_at = image.updated_at,
                                variant_id = image.variant_id,
                                variant_ids = image.variant_ids,
                                width = image.width
                            };
                            // new imge, add it to database
                            image_to_add.database_created = DateTime.Now;
                            image_to_add.database_updated = DateTime.Now;
                            context.ShopifyImages.Add(image_to_add);
                        }
                        Save();
                        count++;
                    }
                    while (count < image.variant_ids.Count);               
                }
            }
        }

        public void RefreshImages()
        {
            PullImages();
            DeleteImages();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}