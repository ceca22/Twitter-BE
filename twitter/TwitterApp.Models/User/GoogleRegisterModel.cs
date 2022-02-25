using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterApp.Models.User
{
    public class GoogleRegisterModel
    {
        
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
    }
}
