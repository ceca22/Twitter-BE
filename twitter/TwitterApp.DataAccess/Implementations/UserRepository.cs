using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;

namespace TwitterApp.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private TwitterAppDbContext _twitterAppDbContext;
        public UserRepository(TwitterAppDbContext twitterAppDbContext)
        {
            _twitterAppDbContext = twitterAppDbContext;
        }
        public void Add(User entity)
        {
            _twitterAppDbContext.Users.Add(entity);

        }

        public void Delete(User entity)
        {
            _twitterAppDbContext.Users.Remove(entity);

        }

        public IQueryable<User> GetAll()
        {
            return _twitterAppDbContext
                .Users
                .Include(x => x.Posts)
                .AsQueryable();
        }

        //public async Task<User> GetById(long id)
        //{
        //    return await _twitterAppDbContext
        //        .Users
        //        .Include(x => x.Posts)
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //}

        public User GetById(long id)
        {
            return _twitterAppDbContext
                .Users
                .Include(x => x.Posts)
                .FirstOrDefault(x => x.Id == id);
        }
        public User GetByExternalId(string id)
        {
            return _twitterAppDbContext
                .Users
                .Include(x => x.Posts)
                .FirstOrDefault(x => x.ExternalId == id);
        }


        public void SaveChanges()
        {
            _twitterAppDbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            _twitterAppDbContext.Users.Update(entity);

        }

        public User Login(string email, string password)
        {
            return _twitterAppDbContext
                .Users
                .FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public User GetByUsername(string username)
        {
            return _twitterAppDbContext
                .Users
                .FirstOrDefault(x => x.Username == username);
        }

        public User GetByEmail(string email)
        {
            return _twitterAppDbContext
                .Users
                .FirstOrDefault(x => x.Email == email);
        }

    }
}
