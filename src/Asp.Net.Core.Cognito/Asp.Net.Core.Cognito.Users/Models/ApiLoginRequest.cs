using System.ComponentModel.DataAnnotations;

namespace Asp.Net.Core.Cognito.Users.Models
{
    public class ApiLoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
