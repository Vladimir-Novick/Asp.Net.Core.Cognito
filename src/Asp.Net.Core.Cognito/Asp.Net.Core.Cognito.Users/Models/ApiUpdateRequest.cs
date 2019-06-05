using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.Net.Core.Cognito.Users.Models
{
    /// <summary>
    ///    User update user information
    /// </summary>
    public class ApiUpdateRequest
    {
        public string UserName { get; set; }
        // 
        public Dictionary<string,string> Attributes { get; set; }
    }
}
