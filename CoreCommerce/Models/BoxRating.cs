using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxRating
    {
        [Key]
        public int box_rating_id { get; set; }

        public Box box { get; set; }

        public decimal rating { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IBoxRatingRepository
    {
        IEnumerable<BoxRating> GetBoxRatings(int box_id);
        BoxRating CreateBoxRating(BoxRating rating);
        BoxRating GetBoxRating(int box_rating_id);
        void UpdateBoxRating(BoxRating rating);
        void DeleteBoxRating(int box_rating_id);
        void Save();
    }

    public class BoxRatingRepository : IBoxRatingRepository
    {
        private ApplicationContext context;

        public BoxRatingRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public BoxRating CreateBoxRating(BoxRating rating)
        {
            rating.created = DateTime.Now;
            rating.updated = DateTime.Now;

            context.BoxRatings.Add(rating);
            Save();

            return rating;
        }

        public void DeleteBoxRating(int box_rating_id)
        {
            context.BoxRatings.Remove(context.BoxRatings.Find(box_rating_id));
        }

        public BoxRating GetBoxRating(int box_rating_id)
        {
            return context.BoxRatings.Find(box_rating_id);
        }

        public IEnumerable<BoxRating> GetBoxRatings(int box_id)
        {
            return context.BoxRatings.Where(x => x.box.box_id == box_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxRating(BoxRating rating)
        {
            rating.updated = DateTime.Now;

            context.Entry(rating).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}