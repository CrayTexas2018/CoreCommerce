using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxComment : CommonFields
    {
        [Key]
        public int comment_id { get; set; }

        public int box_id { get; set; }

        [ForeignKey("box_id")]
        [JsonIgnore]
        [Column("box_id")]
        public Box box { get; set; }

        public int user_id { get; set; }

        [ForeignKey("user_id")]
        [JsonIgnore]
        [Column("user_id")]
        public User user { get; set; }

        public string comment { get; set; }

        public int? reply_id { get; set; }

        [ForeignKey("reply_id")]
        [JsonIgnore]
        public BoxComment reply_comment { get; set; }

        public int score { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostBoxComment
    {
        public int box_id { get; set; }

        public int user_id { get; set; }

        public int? reply_id { get; set; }

        public string comment { get; set; }
    }

    public interface IBoxCommentRepository
    {
        IEnumerable<BoxComment> GetBoxComments(int box_id);
        BoxComment GetBoxCommentById(int? comment_id);
        BoxComment CreateBoxComment(PostBoxComment comment);
        void UpdateBoxComment(BoxComment comment);
        void DeleteBoxComment(int comment_id);
        void UpvoteComment(int comment_id);
        void DownvoteComment(int comment_id);
        void Save();
    }

    public class BoxCommentRepository : IBoxCommentRepository
    {
        private ApplicationContext context;

        public BoxCommentRepository (ApplicationContext context)
        {
            this.context = context;
        }

        public BoxComment CreateBoxComment(PostBoxComment postComment)
        {
            BoxRepository br = new BoxRepository(context);
            UserRepository ur = new UserRepository(context);
            BoxCommentRepository bcr = new BoxCommentRepository(context);
            CompanyRepository cr = new CompanyRepository(context);

            BoxComment comment = new BoxComment
            {
                active = true,
                box_id = br.GetBox(postComment.box_id).box_id,
                comment = postComment.comment,
                score = 0,
                reply_id = postComment.reply_id,
                user_id = postComment.user_id,
                company_id = cr.GetCompanyIdFromApiUser(),
                created = DateTime.Now,
                updated = DateTime.Now
            };

            context.BoxComments.Add(comment);
            Save();

            return comment;
        }

        public void DeleteBoxComment(int comment_id)
        {
            // see if comment exists for company
            BoxComment comment = GetBoxCommentById(comment_id);
            if (comment != null)
            {
                context.BoxComments.Remove(context.BoxComments.Find(comment_id));
                Save();
                return;
            }
            throw new Exception("Box Comment ID not found");
        }

        public void DownvoteComment(int comment_id)
        {
            BoxComment comment = GetBoxCommentById(comment_id);
            comment.score = comment.score - 1;
            UpdateBoxComment(comment);
        }

        public BoxComment GetBoxCommentById(int? comment_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            BoxComment comment = context.BoxComments.Where(x => x.comment_id == comment_id).Where(x => x.comment_id == company_id).FirstOrDefault();

            if (comment != null)
            {
                return comment;
            }
            throw new Exception("Box comment with ID " + comment_id + " not found.");
        }

        public IEnumerable<BoxComment> GetBoxComments(int box_id)
        {
            CompanyRepository cr = new CompanyRepository(context);
            int company_id = cr.GetCompanyIdFromApiUser();
            return context.BoxComments.Where(x => x.box.box_id == box_id).Where(x => x.company_id == company_id).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxComment(BoxComment comment)
        {
            // make sure comment belongs to current company
            comment = GetBoxCommentById(comment.comment_id);
            if (comment != null)
            {
                comment.updated = DateTime.Now;

                context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
                Save();
                return;
            }
            // Should never reach this line b/c getboxcommentbyid should throw error
            throw new Exception("Cannot update box comment with ID " + comment.comment_id + ".");
        }

        public void UpvoteComment(int comment_id)
        {
            BoxComment comment = GetBoxCommentById(comment_id);
            comment.score++;
            UpdateBoxComment(comment);
        }
    }
}