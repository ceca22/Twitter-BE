using System;
using System.Collections.Generic;
using System.Linq;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;
using TwitterApp.Mappers;
using TwitterApp.Models.Post;
using TwitterApp.Models.User;
using TwitterApp.Services.Interfaces;
using TwitterApp.Shared.Exceptions;

namespace TwitterApp.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostDetailsRepository _postDetailsRepository;



        public PostService(IPostRepository postRepository,
            IUserRepository userRepository, 
            IPostDetailsRepository postDetailsRepository
            )
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _postDetailsRepository = postDetailsRepository;
            
        }
        public void AddEntity(PostModel post, string id)
        {

            User userDb = _userRepository.GetByExternalId(id);

            ValidatePost(post);

            Post createPost;

            if (post.Image != null)
            {
                createPost = new Post()
                {
                    UserId = userDb.Id,
                    PostDetails = new List<PostDetails>()
                {
                    new PostDetails()
                    {
                        Text = post.Text,
                        Image = post.Image,
                        DatePosted = DateTime.Now,
                    }
                }.ToList()
                };
            }
            else
            {
                createPost = new Post()
                {
                    UserId = userDb.Id,
                    PostDetails = new List<PostDetails>()
                    {
                    new PostDetails()
                    {
                        Text = post.Text,
                        Image = post.Image,
                        DatePosted = DateTime.Now,
                    }
                }.ToList()

                };
            }

            _postRepository.Add(createPost);
            _postRepository.SaveChanges();
            _postDetailsRepository.SaveChanges();
        }


        public void AddEntityNormalId(PostModel post, string id)
        {

            User userDb =  _userRepository.GetById(int.Parse(id));

            ValidatePost(post);
            Post createPost;

            if (post.Image != null)
            {
                createPost = new Post()
                {
                    UserId = userDb.Id,
                    PostDetails = new List<PostDetails>()
                {
                    new PostDetails()
                    {
                        Text = post.Text,
                        Image = post.Image,
                        DatePosted = DateTime.Now,



                    }
                }.ToList()
                };
            }
            else
            {
                createPost = new Post()
                {
                    UserId = userDb.Id,
                    PostDetails = new List<PostDetails>()
                    {
                    new PostDetails()
                    {
                        Text = post.Text,
                        Image = null,
                        DatePosted = DateTime.Now,

                    }
                }.ToList()

                };
            }

            _postRepository.Add(createPost);
            _postRepository.SaveChanges();
            _postDetailsRepository.SaveChanges();
        }

        //public async Task AddEntityNormalId(PostModel post, string id)
        //{

        //    User userDb = await _userRepository.GetById(int.Parse(id));

        //    ValidatePost(post);
        //    Post createPost;

        //    if (post.Image != null)
        //    {
        //        createPost = new Post()
        //        {
        //            UserId = userDb.Id,
        //            PostDetails = new List<PostDetails>()
        //        {
        //            new PostDetails()
        //            {
        //                Text = post.Text,
        //                Image = post.Image,
        //                DatePosted = DateTime.Now,



        //            }
        //        }.ToList()
        //        };
        //    }
        //    else
        //    {
        //        createPost = new Post()
        //        {
        //            UserId = userDb.Id,
        //            PostDetails = new List<PostDetails>()
        //            {
        //            new PostDetails()
        //            {
        //                Text = post.Text,
        //                Image = null,
        //                DatePosted = DateTime.Now,

        //            }
        //        }.ToList()

        //        };
        //    }

        //    _postRepository.Add(createPost);
        //    _postRepository.SaveChanges();
        //    _postDetailsRepository.SaveChanges();
        //}
        private void ValidatePost(PostModel post)
        {
            if(post.Image == null && post.Text == null)
            {
                throw new PostException("Post cannot be empty!");
            }
        }

        public void DeleteEntity(int id)
        {
            Post deletePost = _postRepository.GetById(id);
            if (deletePost == null)
            {
                throw new NotFoundException($"Post with id {id} was not found");
            }
            _postRepository.Delete(deletePost);
            _postRepository.SaveChanges();
        }

        //public async Task<List<PostModel>> GetAllEntities()
        //{
        //    List<Post> postsDb = _postRepository.GetAll().ToList();
        //    List<PostModel> postModels = new List<PostModel>();
        //    foreach (Post post in postsDb)
        //    {
        //        RegisterModel user = await _userRepository.GetById(post.UserId);
        //        Register Model userRegister = user.ToRegisterModel();
        //        PostDetails postDetails = _postDetailsRepository.GetById(post.Id);
        //        //ImageModel image = _imageRepository.GetById(postDetails.ImageId).ToImageModel();
        //        postModels.Add(post.ToPostModel(postDetails, user));
        //    }

        //    List<PostModel> posts = postModels.OrderByDescending(x => x.DatePosted).ToList();
        //    return posts;
        //}

        public List<PostModel> GetAllEntities()
        {
            List<Post> postsDb = _postRepository.GetAll().OrderByDescending(x => x.Id).ToList();
            List<PostModel> postModels = new List<PostModel>();
            foreach (Post post in postsDb)
            {
                RegisterModel user = _userRepository.GetById(post.UserId).ToRegisterModel();
                PostDetails postDetails = _postDetailsRepository.GetById(post.Id);
                
                postModels.Add(post.ToPostModel(postDetails, user));
            }

            //List<PostModel> posts = postModels.OrderByDescending(x => x.DatePosted).ToList();
            return postModels;
        }

        public List<PostModel> GetAll(int take, int skip )
        {
            
            List<Post> postsDb = _postRepository.GetAll().OrderByDescending(x => x.Id).Skip(skip).Take(take).ToList();
            List<PostModel> postModels = new List<PostModel>();
            if(postsDb.Count != 0)
            {
                foreach (Post post in postsDb)
                {
                    RegisterModel user = _userRepository.GetById(post.UserId).ToRegisterModel();
                    PostDetails postDetails = _postDetailsRepository.GetById(post.Id);

                    postModels.Add(post.ToPostModel(postDetails, user));
                }

                //List<PostModel> posts = postModels.OrderByDescending(x => x.DatePosted).ToList();
                return postModels;
            }
            return null;
        }

        public List<PostModel> CurrentUserPosts(string id)
        {
            RegisterModel user = _userRepository.GetById(int.Parse(id)).ToRegisterModel();
            List<Post> posts = _postRepository.GetAll().ToList();
            List<Post> userPosts = posts.Where(x => x.UserId == user.Id).ToList();

            List<PostDetails> postDetailsList = _postDetailsRepository.GetAll().ToList();
            List<PostModel> postModelList = new List<PostModel>();

            foreach(Post post in userPosts)
            {
                foreach(PostDetails postDetails in postDetailsList)
                {
                    if(postDetails.PostId == post.Id)
                    {
                        
                        postModelList.Add(post.ToPostModel(postDetails,user));
                    }
                }
            }

            return postModelList;
        }

        public PostModel GetEntityById(int id)
        {
            Post postDb = _postRepository.GetById(id);
            if (postDb == null)
            {
                throw new NotFoundException($"Post with id {id} was not found");
            }

            RegisterModel user = _userRepository.GetById(postDb.UserId).ToRegisterModel();
            PostDetails postDetails = _postDetailsRepository.GetById(postDb.Id);
            //ImageModel image = _imageRepository.GetById(postDetails.ImageId).ToImageModel();
            return postDb.ToPostModel(postDetails, user);
        }

        public void UpdateEntity(PostModel post)
        {
            PostDetails postDb = _postDetailsRepository.GetById(post.Id);
            if (postDb == null)
            {
                throw new NotFoundException($"User with Id {post.Id} was not found");
            }

            if (string.IsNullOrEmpty(post.Text) && post.Image == null)
            {
                throw new PostException("Input invalid!Enter text or image to post!");
            }
            if (post.Text.Length > 280)
            {
                throw new PostException("The property Text can't be longer than 280 characters!");
            }

            postDb.Text = post.Text;
            

            _postDetailsRepository.Update(postDb);
            _postDetailsRepository.SaveChanges();
        }
    }
}
