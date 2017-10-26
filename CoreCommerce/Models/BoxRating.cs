using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxRating : CommonFields
    {
        [Key]
        public int box_rating_id { get; set; }

        [Index(IsUnique = true, Order = 0)]
        public int box_id { get; set; }

        [ForeignKey("box_id")]
        [JsonIgnore]
        [Column("box_id")]
        public Box box { get; set; }

        [Index(IsUnique = true, Order = 1)]
        public int user_id { get; set; }

        [ForeignKey("user_id")]
        [JsonIgnore]
        [Column("user_id")]
        public User user { get; set; }

        public decimal rating { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostBoxRating
    {
        public int user_id { get; set; }

        public int box_id { get; set; }

        public decimal rating { get; set; }
    }

    public interface IBoxRatingRepository
    {
        IEnumerable<BoxRating> GetBoxRatings(int box_id);
        BoxRating CreateBoxRating(PostBoxRating rating);
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

        public BoxRating CreateBoxRating(PostBoxRating postRating)
        {
            BoxRepository br = new BoxRepository(context);
            UserRepository ur = new UserRepository(context);

            BoxRating rating = new BoxRating();
            rating.box = br.GetBox(postRating.box_id);
            rating.user = ur.GetUserById(postRating.user_id);
            rating.user_id = postRating.user_id;
            rating.rating = postRating.rating;
            rating.active = true;
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