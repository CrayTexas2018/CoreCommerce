using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class ItemRating
    {
        [Key]
        public int item_rating_id { get; set; }

        public User user { get; set; }

        public Item item { get; set; }

        public int rating { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IItemRatingRepository
    {
        IEnumerable<ItemRating> GetItemRatings(int item_id);
        ItemRating GetItemRating(int rating_id);
        ItemRating CreateItemRating(ItemRating rating);
        void UpdateItemRating(ItemRating rating);
        void DeleteItemRating(int rating_id);
        void Save();
    }

    public class ItemRatingRepository : IItemRatingRepository
    {
        private ApplicationContext context;

        public ItemRatingRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public ItemRating CreateItemRating(ItemRating rating)
        {
            rating.created = DateTime.Now;
            rating.updated = DateTime.Now;

            context.ItemRatings.Add(rating);
            Save();

            return rating;
        }

        public void DeleteItemRating(int rating_id)
        {
            context.ItemRatings.Remove(context.ItemRatings.Find(rating_id));
        }

        public ItemRating GetItemRating(int rating_id)
        {
            return context.ItemRatings.Find(rating_id);
        }

        public IEnumerable<ItemRating> GetItemRatings(int item_id)
        {
            return context.ItemRatings.Where(x => x.item.item_id == item_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateItemRating(ItemRating rating)
        {
            rating.updated = DateTime.Now;

            context.Entry(rating).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}