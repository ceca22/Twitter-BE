using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterApp.Shared.Exceptions
{
    public class PostException:Exception
    {
        public PostException(string message) : base(message)
        {

        }
    }
}
