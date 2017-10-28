using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CoreCommerce.Models
{
    public class ShopifyManager
    {
        private ApplicationContext context;

        public ShopifyManager(ApplicationContext context)
        {
            this.context = context;
        }

        public string shopifyGetRequest(string endpoint)
        {
            // Get all products from shopify
            //string url = "https://bee3869c2811039606dad0f0d819e543:9d8c95ebd2408e11d3ac87d3e5d5c65f@core-commerce-dev.myshopify.com/admin/products.json";
            string json = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            string username = "bee3869c2811039606dad0f0d819e543";
            string password = "9d8c95ebd2408e11d3ac87d3e5d5c65f";
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            request.Headers.Add("Authorization", "Basic " + svcCredentials);

            WebResponse response = request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                json = reader.ReadToEnd();
            }

            return json;
        }

        public void addOrUpdateShopifyProducts()
        {
            string json = shopifyGetRequest("https://bee3869c2811039606dad0f0d819e543:9d8c95ebd2408e11d3ac87d3e5d5c65f@core-commerce-dev.myshopify.com/admin/products.json");

            RootObject jsonObject = JsonConvert.DeserializeObject<RootObject>(json);
            
            if (jsonObject != null)
            {
                updateProducts(jsonObject.products);
            }
        }

        public void updateProducts(List<Product> products)
        {
            foreach (Product p in products)
            {
                // See if product exists in database
                Product existingProduct = context.ShopifyProducts.Find(p.Id);

                if (existingProduct != null)
                {
                    context.Entry(existingProduct).CurrentValues.SetValues(p);
                }
                else
                {
                    // Product does not exist, add it
                    Product newProduct = new Product
                    {
                        body_html = p.body_html,
                        company = p.company,
                        company_id = p.company_id,
                        created_at = p.created_at,
                        handle = p.handle,
                        Id = p.Id,
                        product_type = p.product_type,
                        published_at = p.published_at,
                        published_scope = p.published_scope,
                        tags= p.tags,
                        template_suffix = p.template_suffix,
                        title = p.title,
                        updated_at = p.updated_at,
                        vendor = p.vendor
                    };
                    context.ShopifyProducts.Add(newProduct);
                }
                context.SaveChanges();

                updateVariants(p.variants);
                updateImages(p.images);

                // Delete old content
                deleteImages(p.images, p.company_id);
                deleteVariants(p.variants, p.company_id);
                deleteProducts(products, p.company_id);
            }
        }

        public void updateVariants(List<Variant> variants)
        {
            foreach (Variant v in variants)
            {
                Variant existingVariant = context.ShopifyVariants.Find(v.Id);

                if (existingVariant != null)
                {
                    context.Entry(existingVariant).CurrentValues.SetValues(v);
                }
                else
                {
                    // Product does not exist, add it
                    context.ShopifyVariants.Add(v);
                }
                context.SaveChanges();
            }
        }

        public void updateImages(List<Image> images)
        {
            foreach (Image i in images)
            {               
                foreach (long id in i.variant_ids)
                {
                    Image image_with_Id = new Image
                    {
                        company = i.company,
                        company_id = i.company_id,
                        created_at = i.created_at,
                        height = i.height,
                        Id = i.Id,
                        position = i.position,
                        src = i.src,
                        updated_at = i.updated_at,
                        variant_id = id,
                        width = i.width                        
                    };

                    if (i.variant_ids.Count == 0)
                    {
                        context.ShopifyImages.Remove(image_with_Id);
                        continue;
                    }

                    Image existingImage = context.ShopifyImages.Find(i.Id, id);

                    if (existingImage != null)
                    {
                        context.Entry(existingImage).CurrentValues.SetValues(image_with_Id);
                    }                    
                    else
                    {
                        // Product does not exist, add it
                        context.ShopifyImages.Add(image_with_Id);
                    }
                    context.SaveChanges();
                }                
            }                        
        }

        public void deleteImages(List<Image> images_to_keep, int company_id)
        {
            // Get a list of all images
            List<Image> allImages = context.ShopifyImages.Where(x => x.company_id == company_id).ToList();
            foreach (Image allImage in allImages)
            {
                bool delete = true;
                foreach (Image i in images_to_keep)
                {
                    if (i.variant_ids.Count == 0)
                    {
                        List<Image> images_to_delete = context.ShopifyImages.Where(x => x.Id == i.Id).ToList();
                        if (images_to_delete != null)
                        {
                            foreach (Image iDelete in images_to_delete)
                            {
                                context.ShopifyImages.Remove(iDelete);
                                context.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        if (i.Id == allImage.Id)
                        {
                            delete = false;
                            continue;
                        }
                    }
                }

                if (delete)
                {
                    // get all images with the id
                    List<Image> images_to_delete = context.ShopifyImages.Where(x => x.Id == allImage.Id).Where(x => x.company_id == company_id).ToList();
                    foreach (Image iDelete in images_to_delete)
                    {
                        context.ShopifyImages.Remove(iDelete);
                        context.SaveChanges();
                    }
                }
            }                        
        }

        public void deleteVariants(List<Variant> variants_to_keep, int company_id)
        {
            // Get all variants
            List<Variant> all_variants = context.ShopifyVariants.Where(x => x.company_id == company_id).ToList();

            foreach (Variant all_variant in all_variants)
            {
                bool delete = true;
                foreach (Variant keep_variant in variants_to_keep)
                {
                    if (keep_variant.Id == all_variant.Id)
                    {
                        delete = false;
                    }
                }

                if (delete)
                {
                    context.ShopifyVariants.Remove(all_variant);
                    context.SaveChanges();
                }
            }
        }

        public void deleteProducts(List<Product> products_to_keep, int company_id)
        {
            // Get all variants
            List<Product> all_products = context.ShopifyProducts.Where(x => x.company_id == company_id).ToList();

            foreach (Product all_product in all_products)
            {
                bool delete = true;
                foreach (Product keep_product in products_to_keep)
                {
                    if (all_product.Id == all_product.Id)
                    {
                        delete = false;
                    }
                }

                if (delete)
                {
                    context.ShopifyProducts.Remove(all_product);
                    context.SaveChanges();
                }
            }
        }
    }
}