using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterApp.Domain.Models
{
    public class Post
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

        public ICollection<PostDetails> PostDetails { get; set; } = new List<PostDetails>();
        public ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
        public ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
        public ICollection<PostShare> PostShares { get; set; } = new List<PostShare>();
        public ICollection<PostRetweet> PostRetweets { get; set; } = new List<PostRetweet>();


    }
}
