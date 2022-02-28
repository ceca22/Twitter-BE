using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.Domain.Models;

namespace TwitterApp.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User Login(string email, string password);

        public User GetByUsername(string username);

        void Update(User entity);
        User GetById(long id);
        //Task<User> GetById(long id);

        User GetByExternalId(string id);

        User GetByEmail(string email);
        IQueryable<User> GetAll();
        void Add(User entity);
        void Delete(User entity);
        void SaveChanges();
    }
}
