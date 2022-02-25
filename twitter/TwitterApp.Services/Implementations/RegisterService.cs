using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;
using TwitterApp.Models.User;
using TwitterApp.Services.Interfaces;
using TwitterApp.Shared.Exceptions;

namespace TwitterApp.Services.Implementations
{
    public class RegisterService:IRegisterService
    {
        private IUserRepository _userRepository;

        public RegisterService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(RegisterModel user)
        {
            ValidateUser(user);

            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(user.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            User createUser = new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                UserComments = new List<UserComment>(),
                UserLikes = new List<UserLike>(),
                Posts = new List<Post>(),
                UserRetweets = new List<UserRetweet>(),
                UserShares = new List<UserShare>(),
                
            };


            _userRepository.Add(createUser);
            _userRepository.SaveChanges();



        }

        private void ValidateUser(RegisterModel user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Email))
            {
                throw new UserException("The properties Username,Password,Firstname and Lastname are required fields");
            }
            if (user.Username.Length > 30)
            {
                throw new UserException("Username can contain max 30 characters");
            }
            if (user.FirstName.Length > 50 || user.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(user.Username))
            {
                throw new UserException("A user with this username already exists!");
            }
            if (user.Password != user.ConfirmPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(user.Password))
            {
                throw new UserException("The password should be more than 5 character and should contain numbers as well!");
            }
        }

        private bool IsUserNameUnique(string username)
        {
            if (_userRepository.GetByUsername(username) != null)
            {
                return false;
            }
            return true;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }


        public void RegisterWithGoogle(GoogleRegisterModel user)
        {
            User userDb = _userRepository.GetByExternalId(user.ExternalId);
            if(userDb == null)
            {
                User createUser = new User()
                {
                    ExternalId = user.ExternalId.ToString(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.Username,
                    Password = null,
                    PhotoUrl = user.PhotoUrl,
                    UserComments = new List<UserComment>(),
                    UserLikes = new List<UserLike>(),
                    Posts = new List<Post>(),
                    UserRetweets = new List<UserRetweet>(),
                    UserShares = new List<UserShare>(),

                };


                _userRepository.Add(createUser);
                _userRepository.SaveChanges();
            }



        }


    }
}
