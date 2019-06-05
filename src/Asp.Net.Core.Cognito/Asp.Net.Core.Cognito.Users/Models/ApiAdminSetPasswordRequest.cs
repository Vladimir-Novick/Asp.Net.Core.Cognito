using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Cognito.Users.Models
{
    public class ApiAdminSetPasswordRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
