using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Cognito.Users.Models
{
    public class ApiChangePasswordRequest
    {
        public string UserName { get; set; }
        public string PreviewPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
