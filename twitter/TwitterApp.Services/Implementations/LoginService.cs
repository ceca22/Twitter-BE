using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TwitterApp.DataAccess.Interfaces;
using TwitterApp.Domain.Models;
using TwitterApp.Models.User;
using TwitterApp.Services.Interfaces;
using TwitterApp.Shared.Custom;
using TwitterApp.Shared.Exceptions;

namespace TwitterApp.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private IOptions<AppSettings> _options;
        public LoginService(IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }
        public string Login(LoginModel user)
        {
            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(user.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);


            string email = _userRepository
                            .GetAll()
                            .Where(x => x.Email == user.Email)
                            .Select(y => y.Email)
                            .ToString();

            string password = _userRepository
                            .GetAll()
                            .Where(x => x.Password == user.Password)
                            .Select(y => y.Password)
                            .ToString();

            ValidateInput(email, password);

            User userDb = _userRepository.Login(user.Email, hashedPassword);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //get the SecretKey from AppSettings
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
            //configure the token
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(10),
                //signature definition
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload   

                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDb.Email),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),

                    }

            )
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            // convert it to string
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            return tokenString;
        }

        private void ValidateInput(string email, string password)
        {
            if (email == null && password != null)
            {
                throw new NotFoundException($"Incorrect username!");
            }
            if (password == null && email != null)
            {
                throw new NotFoundException($"Incorrect password!");
            }
            if (password == null && email == null)
            {
                throw new NotFoundException($"No User found!");
            }

        }
    }
}
