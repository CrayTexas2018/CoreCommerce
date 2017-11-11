using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class ItemComment : CommonFields
    {
        [Key]
        public int comment_id { get; set; }

        public int item_id { get; set; }

        [ForeignKey("item_id")]
        [Column("item_id")]
        public Item item { get; set; }

        public int user_id { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        public User user { get; set; }

        public string comment { get; set; }

        public int upvotes { get; set; }

        public int downvotes { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostItemComment
    {
        public int item_id { get; set; }

        public int user_id { get; set; }

        public string comment { get; set; }
    }

    public interface IItemCommentRepository
    {
        IEnumerable<ItemComment> GetItemComments(int item_id);
        ItemComment GetComment(int comment_id);
        ItemComment CreateComment(PostItemComment comment);
        void UpdateComment(ItemComment comment);
        void DeleteComment(int comment_id);
        void Save();
    }

    public class ItemCommentRepository : IItemCommentRepository
    {
        private ApplicationContext context;

        public ItemCommentRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public ItemComment CreateComment(PostItemComment postComment)
        {
            ItemRepository ir = new ItemRepository(context);
            UserRepository ur = new UserRepository(context);
            CompanyRepository cr = new CompanyRepository(context);

            ItemComment comment = new ItemComment
            {
                active = true,
                comment = postComment.comment,
                company_id = cr.GetCompanyIdFromApiUser(),
                downvotes = 0,
                item_id = postComment.item_id,
                created = DateTime.Now,
                updated = DateTime.Now,
                upvotes = 0,
                user_id = postComment.user_id
            };

            context.ItemComments.Add(comment);
            Save();

            return comment;
        }

        public void DeleteComment(int comment_id)
        {
            ItemComment comment = GetComment(comment_id);
            context.ItemComments.Remove(comment);
            Save();
        }

        public ItemComment GetComment(int comment_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();

            ItemComment comment = context.ItemComments.Where(x => x.comment_id == comment_id).Where(x => x.company_id == company_id).FirstOrDefault();
            if (comment != null)
            {
                return comment;
            }
            throw new Exception("Item comment ID " + comment.comment_id + " not found");
        }

        public IEnumerable<ItemComment> GetItemComments(int item_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.ItemComments.Where(x => x.item.item_id == item_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateComment(ItemComment comment)
        {
            ItemComment verify = GetComment(comment.comment_id);

            if (verify != null)
            {
                comment.updated = DateTime.Now;

                context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                Save();
            }
        }
    }
}