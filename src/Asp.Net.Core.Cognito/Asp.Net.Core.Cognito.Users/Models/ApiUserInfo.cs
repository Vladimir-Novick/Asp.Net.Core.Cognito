using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Cognito.Users.Models
{
    /// <summary>
    ///    User registration information
    /// </summary>
    public class ApiUserInfo
    {
        public string Name { get; set; }
        public string Status { get; set; }
        // 
        public Dictionary<string,string> Attributes { get; set; }

    }
}
