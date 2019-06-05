using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Cognito.Users.Models
{
    /// <summary>
    ///    User registration information
    /// </summary>
    public class ApiRegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string eMail { get; set;  }
        // 
        public Dictionary<string,string> Attributes { get; set; }
    }
}
