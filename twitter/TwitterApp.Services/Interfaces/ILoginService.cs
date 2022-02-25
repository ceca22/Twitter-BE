using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Models.User;

namespace TwitterApp.Services.Interfaces
{
    public interface ILoginService
    {
        string Login(LoginModel user);

    }
}
