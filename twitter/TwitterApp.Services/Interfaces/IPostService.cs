using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TwitterApp.Models.Post;

namespace TwitterApp.Services.Interfaces
{
    public interface IPostService
    {
        void AddEntity(PostModel post, string id);

        void DeleteEntity(int id);

        List<PostModel> GetAllEntities();

        PostModel GetEntityById(int id);

        void UpdateEntity(PostModel post);

        List<PostModel> CurrentUserPosts(string id);

        void AddEntityNormalId(PostModel post, string id);
        List<PostModel> GetAll(int take, int skip);

        //Task AddEntityNormalId(PostModel post, string id);

    }
}
