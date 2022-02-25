using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;

namespace TwitterApp.DataAccess.Implementations
{
    public class PostDetailsRepository : IPostDetailsRepository
    {
        private TwitterAppDbContext _twitterAppDbContext;
        public PostDetailsRepository(TwitterAppDbContext twitterAppDbContext)
        {
            _twitterAppDbContext = twitterAppDbContext;
        }

        public void Add(PostDetails entity)
        {
            _twitterAppDbContext.PostDetails.Add(entity);
        }

        public void Delete(PostDetails entity)
        {
            _twitterAppDbContext.PostDetails.Remove(entity);
        }

        public IQueryable<PostDetails> GetAll()
        {
            return _twitterAppDbContext
               .PostDetails
               .AsQueryable();
        }

        public PostDetails GetById(int id)
        {
            return _twitterAppDbContext
                .PostDetails
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _twitterAppDbContext.SaveChanges();
        }

        public void Update(PostDetails entity)
        {
            _twitterAppDbContext.PostDetails.Update(entity);
        }
    }
}
