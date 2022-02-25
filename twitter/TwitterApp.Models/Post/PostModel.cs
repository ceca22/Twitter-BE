using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Models.Image;
using TwitterApp.Models.User;
using static System.Net.Mime.MediaTypeNames;

namespace TwitterApp.Models.Post
{
    public class PostModel
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public RegisterModel User { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
