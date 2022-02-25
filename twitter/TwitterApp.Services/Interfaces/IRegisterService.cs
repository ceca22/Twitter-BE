using System;
using System.Collections.Generic;
using System.Text;
using TwitterApp.Models.User;

namespace TwitterApp.Services.Interfaces
{
    public interface IRegisterService
    {
        void Register(RegisterModel user);

        void RegisterWithGoogle(GoogleRegisterModel user);

    }
}
