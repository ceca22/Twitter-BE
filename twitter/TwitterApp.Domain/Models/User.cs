using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwitterApp.Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();
        public ICollection<UserLike> UserLikes { get; set; } = new List<UserLike>();
        public ICollection<UserShare> UserShares { get; set; } = new List<UserShare>();
        public ICollection<UserRetweet> UserRetweets { get; set; } = new List<UserRetweet>();




    }
}
