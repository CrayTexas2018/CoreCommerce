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
            UserRepository ur = new UserRepository(context);
            CompanyRepository cr = new CompanyRepository(context);

            // make sure user belongs to company
            ur.GetUserById(postRating.user_id);

            BoxRating rating = new BoxRating
            {
                active = true,
                box_id = postRating.box_id,
                company_id = cr.GetCompanyIdFromApiUser(),
                rating = postRating.rating,
                user_id = postRating.user_id,
                created = DateTime.Now,
                updated = DateTime.Now
            };

            context.BoxRatings.Add(rating);
            Save();
            return rating;
        }

        public void DeleteBoxRating(int box_rating_id)
        {
            // Get box rating to make sure it belongs to company
            BoxRating br = GetBoxRating(box_rating_id);

            context.BoxRatings.Remove(br);
            Save();
        }

        public BoxRating GetBoxRating(int box_rating_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            BoxRating br = context.BoxRatings.Where(x => x.box_rating_id == box_rating_id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (br != null)
            {
                return br;
            }
            throw new Exception("Box rating ID " + br.box_rating_id + " not found.");
        }

        public IEnumerable<BoxRating> GetBoxRatings(int box_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            return context.BoxRatings.Where(x => x.box.box_id == box_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxRating(BoxRating rating)
        {
            // make sure box rating exists for company
            BoxRating verify = GetBoxRating(rating.box_rating_id);

            rating.updated = DateTime.Now;

            context.Entry(rating).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}