using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Domain.Models;
using TwitterApp.Models.Image;
using TwitterApp.Models.Post;
using TwitterApp.Models.User;

namespace TwitterApp.Mappers
{
    public static class PostMapper
    {



        public static PostModel ToPostModel(this Post post, PostDetails postDetails, RegisterModel user)
        {

            return new PostModel
            {
                Id = post.Id,
                UserFullName = $"{ user.FirstName } { user.LastName }",
                User = user,
                Text = postDetails.Text,
                Image = postDetails.Image,
                DatePosted = postDetails.DatePosted
               

            };
        }
    }
}
