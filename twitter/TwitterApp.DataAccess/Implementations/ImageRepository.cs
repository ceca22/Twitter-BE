using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;

namespace TwitterApp.DataAccess.Implementations
{
    public class ImageRepository
    {
        private TwitterAppDbContext _twitterAppDbContext;
        public ImageRepository(TwitterAppDbContext twitterAppDbContext)
        {
            _twitterAppDbContext = twitterAppDbContext;
        }
        //public void Add(Image entity)
        //{
        //    _twitterAppDbContext.Images.Update(entity);

        //}

        //public void Delete(Image entity)
        //{
        //    _twitterAppDbContext.Images.Remove(entity);

        //}

        //public IQueryable<Image> GetAll()
        //{
        //    return _twitterAppDbContext
        //        .Images
        //        .AsQueryable();
        //}

        //public Image GetById(int id)
        //{
        //    return _twitterAppDbContext
        //        .Images
        //        .FirstOrDefault(x => x.Id == id);
        //}

        //public void SaveChanges()
        //{
        //    _twitterAppDbContext.SaveChanges();
        //}

        //public void Update(Image entity)
        //{
        //    _twitterAppDbContext.Images.Update(entity);

        //}
    }
}
