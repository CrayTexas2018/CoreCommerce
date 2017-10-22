using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxComment
    {
        [Key]
        public int comment_id { get; set; }

        public Box box { get; set; }

        public User user { get; set; }

        public string comment { get; set; }

        public int upvotes { get; set; }

        public int downvotes { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public interface IBoxCommentRepository
    {
        IEnumerable<BoxComment> GetBoxComments(int box_id);
        BoxComment GetBoxCommentById(int comment_id);
        BoxComment CreateBoxComment(BoxComment comment);
        void UpdateBoxComment(BoxComment comment);
        void DeleteBoxComment(int comment_id);
        void Save();
    }

    public class BoxCommentRepository : IBoxCommentRepository
    {
        private ApplicationContext context;

        public BoxCommentRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public BoxComment CreateBoxComment(BoxComment comment)
        {
            comment.created = DateTime.Now;
            comment.updated = DateTime.Now;

            context.BoxComments.Add(comment);
            Save();

            return comment;
        }

        public void DeleteBoxComment(int comment_id)
        {
            context.BoxComments.Remove(context.BoxComments.Find(comment_id));
        }

        public BoxComment GetBoxCommentById(int comment_id)
        {
            return context.BoxComments.Find(comment_id);
        }

        public IEnumerable<BoxComment> GetBoxComments(int box_id)
        {
            return context.BoxComments.Where(x => x.box.box_id == box_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxComment(BoxComment comment)
        {
            comment.updated = DateTime.Now;

            context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}