using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class ItemComment
    {
        [Key]
        public int comment_id { get; set; }

        public Item item { get; set; }

        public User user { get; set; }

        public string comment { get; set; }

        public int upvotes { get; set; }

        public int downvotes { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IItemCommentRepository
    {
        IEnumerable<ItemComment> GetItemComments(int item_id);
        ItemComment GetComment(int comment_id);
        ItemComment CreateComment(ItemComment comment);
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

        public ItemComment CreateComment(ItemComment comment)
        {
            comment.created = DateTime.Now;
            comment.updated = DateTime.Now;

            context.ItemComments.Add(comment);
            Save();

            return comment;
        }

        public void DeleteComment(int comment_id)
        {
            context.ItemComments.Remove(context.ItemComments.Find(comment_id));
        }

        public ItemComment GetComment(int comment_id)
        {
            return context.ItemComments.Find(comment_id);
        }

        public IEnumerable<ItemComment> GetItemComments(int item_id)
        {
            return context.ItemComments.Where(x => x.item.item_id == item_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateComment(ItemComment comment)
        {
            comment.updated = DateTime.Now;

            context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}