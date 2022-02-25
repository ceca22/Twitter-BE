using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;
using TwitterApp.Mappers;
using TwitterApp.Models.User;
using TwitterApp.Services.Interfaces;
using TwitterApp.Shared.Exceptions;

namespace TwitterApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddEntity(RegisterModel user)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            User deleteUser = _userRepository.GetById(id);
            if (deleteUser == null)
            {
                throw new NotFoundException($"User with Id {id} was not found");
            }
            _userRepository.Delete(deleteUser);
        }

        public List<RegisterModel> GetAllEntities()
        {
            List<User> userDb = _userRepository.GetAll().ToList();
            List<RegisterModel> users = new List<RegisterModel>();
            foreach (User user in userDb)
            {
                users.Add(user.ToRegisterModel());
            }
            return users;
        }

        public RegisterModel GetEntityById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with id {id} was not found");
            }
            return userDb.ToRegisterModel();
        }

        public RegisterModel GetEntityByExternalId(string id)
        {
            User userDb = _userRepository.GetByExternalId(id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with id {id} was not found");
            }
            return userDb.ToRegisterModel();
        }

        public void UpdateEntity(RegisterModel user)
        {
            User userDb = _userRepository.GetById(user.Id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with Id {user.Id} was not found");
            }

            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                throw new UserException("The properties Username,Password,Firstname and Lastname are required");
            }
            if (user.Email.Length > 50)
            {
                throw new UserException("The property Username can't be longer than 50 characters!");
            }
            if (user.FirstName.Length > 50 || user.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }


            if (string.IsNullOrEmpty(user.ExternalId))
            {
                if (!IsPasswordValid(user.Password))
                {
                    throw new UserException("The password should be more than 5 character and must contain numbers as well!");
                }

                var md5 = new MD5CryptoServiceProvider();

                var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(user.Password));

                var hashedPassword = Encoding.ASCII.GetString(md5Data);
                userDb.Password = hashedPassword;
            }
           

            

            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Username = user.Username;
            

            _userRepository.Update(userDb);
            _userRepository.SaveChanges();
        }

        //public async Task UpdateEntity(RegisterModel user)
        //{
        //    User userDb = await _userRepository.GetById(user.Id);
        //    if (userDb == null)
        //    {
        //        throw new NotFoundException($"User with Id {user.Id} was not found");
        //    }

        //    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
        //    {
        //        throw new UserException("The properties Username,Password,Firstname and Lastname are required");
        //    }
        //    if (user.Email.Length > 50)
        //    {
        //        throw new UserException("The property Username can't be longer than 50 characters!");
        //    }
        //    if (user.FirstName.Length > 50 || user.LastName.Length > 50)
        //    {
        //        throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
        //    }


        //    if (string.IsNullOrEmpty(user.ExternalId))
        //    {
        //        if (!IsPasswordValid(user.Password))
        //        {
        //            throw new UserException("The password should be more than 5 character and must contain numbers as well!");
        //        }

        //        var md5 = new MD5CryptoServiceProvider();

        //        var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(user.Password));

        //        var hashedPassword = Encoding.ASCII.GetString(md5Data);
        //        userDb.Password = hashedPassword;
        //    }




        //    userDb.FirstName = user.FirstName;
        //    userDb.LastName = user.LastName;
        //    userDb.Email = user.Email;
        //    userDb.Username = user.Username;


        //    _userRepository.Update(userDb);
        //    _userRepository.SaveChanges();
        //}

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
    }
}
