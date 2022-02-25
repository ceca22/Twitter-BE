using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Domain.Models;
using TwitterApp.Models.User;

namespace TwitterApp.Mappers
{
    public static class UserMapper
    {

        public static User ToUser(this RegisterModel user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                ExternalId = user.ExternalId,
                PhotoUrl = user.PhotoUrl

            };
        }

        public static RegisterModel ToRegisterModel(this User user)
        {
            return new RegisterModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                ConfirmPassword = user.Password,
                ExternalId = user.ExternalId,
                PhotoUrl = user.PhotoUrl

            };
        }


    }
}
