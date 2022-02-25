using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;

namespace TwitterApp.DataAccess.Implementations
{
    public class PostRepository : IPostRepository
    {
        private TwitterAppDbContext _twitterAppDbContext;
        public PostRepository(TwitterAppDbContext twitterAppDbContext)
        {
            _twitterAppDbContext = twitterAppDbContext;
        }
        public void Add(Post entity)
        {
            _twitterAppDbContext.Posts.Add(entity);

        }

        public void Delete(Post entity)
        {
            _twitterAppDbContext.Posts.Remove(entity);

        }

        public IQueryable<Post> GetAll()
        {
            return _twitterAppDbContext
                .Posts
                .Include(x => x.PostDetails)
                .AsQueryable();
        }

        

        public Post GetById(int id)
        {
            return _twitterAppDbContext
                .Posts
                .Include(x => x.PostDetails)
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _twitterAppDbContext.SaveChanges();
        }

        public void Update(Post entity)
        {
            _twitterAppDbContext.Posts.Update(entity);

        }
    }
}
