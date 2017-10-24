using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoreCommerce.Models
{
    public class BoxCommentVote
    {
        [Key]
        public int box_comment_vote_id { get; set; }

        public int user_id { get; set; }

        [JsonIgnore]
        [Index(IsUnique = true, Order = 0)]
        public User user { get; set; }

        public int box_comment_id { get; set; }

        [ForeignKey("box_comment_id")]
        [JsonIgnore]
        [Index(IsUnique = true, Order = 1)]
        public BoxComment box_comment { get; set; }

        public bool is_upvote { get; set; }

        public bool active { get; set; }

        public DateTime created { get; set; }

        public DateTime updated { get; set; }
    }

    public class PostBoxCommentVote
    {        
        public int user_id { get; set; }

        public int box_comment_id { get; set; }

        public bool is_upvote { get; set; }
    }

    public interface IBoxCommentVoteRepository
    {
        IEnumerable<BoxCommentVote> GetUserVotes(int user_id);
        BoxCommentVote CreateBoxCommentVote(PostBoxCommentVote postVote);
        BoxCommentVote GetVote(int vote_id);
        IEnumerable<BoxCommentVote> GetCommentVotes(int comment_id);
        void UpdateBoxCommentVote(BoxCommentVote vote);
        void DeleteBoxCommentVote(int vote_id);
        void Save();
    }

    public class BoxCommentVoteRepository : IBoxCommentVoteRepository
    {
        private ApplicationContext context;

        public BoxCommentVoteRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public BoxCommentVote CreateBoxCommentVote(PostBoxCommentVote postVote)
        {
            BoxCommentRepository bcr = new BoxCommentRepository(context);
            UserRepository ur = new UserRepository(context);

            // See if upvote already exists
            if (checkIfCommentExists(postVote.user_id, postVote.box_comment_id))
            {
                throw new Exception("User_id: " + postVote.user_id + " has already placed vote for this comment. Use update instead of create");
            }

            BoxCommentVote vote = new BoxCommentVote
            {
                user_id = postVote.user_id,
                box_comment_id = postVote.box_comment_id,
                is_upvote = postVote.is_upvote
            };

            vote.active = true;
            vote.box_comment = bcr.GetBoxCommentById(postVote.box_comment_id);
            vote.created = DateTime.Now;
            vote.updated = DateTime.Now;
            vote.user = ur.GetUserById(postVote.user_id);

            context.BoxCommentVotes.Add(vote);
            Save();

            // Upvote the comment
            BoxComment comment = vote.box_comment;
            if (vote.is_upvote)
            {
                bcr.UpvoteComment(vote.box_comment_id);
            }
            else
            {
                bcr.DownvoteComment(vote.box_comment_id);
            }

            return vote;
        }

        public void DeleteBoxCommentVote(int vote_id)
        {
            context.BoxCommentVotes.Remove(context.BoxCommentVotes.Find(vote_id));
        }

        public IEnumerable<BoxCommentVote> GetCommentVotes(int comment_id)
        {
            return context.BoxCommentVotes.Where(x => x.box_comment_id == comment_id).ToList();
        }

        public IEnumerable<BoxCommentVote> GetUserVotes(int user_id)
        {
            return context.BoxCommentVotes.Where(x => x.user_id == user_id).ToList();
        }

        public BoxCommentVote GetVote(int vote_id)
        {
            return context.BoxCommentVotes.Find(vote_id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateBoxCommentVote(BoxCommentVote vote)
        {
            BoxCommentRepository bcr = new BoxCommentRepository(context);

            context.Entry(vote).State = System.Data.Entity.EntityState.Modified;
            Save();

            if (vote.is_upvote)
            {
                bcr.UpvoteComment(vote.box_comment_id);
            }
            else
            {
                bcr.DownvoteComment(vote.box_comment_id);
            }
        }

        private bool checkIfCommentExists(int user_id, int comment_id)
        {
            BoxCommentVote vote = context.BoxCommentVotes.Where(x => x.user_id == user_id).Where(x => x.box_comment_id == comment_id).FirstOrDefault();
            if (vote == null)
            {
                return false;
            }
            return true;
        }
    }
}