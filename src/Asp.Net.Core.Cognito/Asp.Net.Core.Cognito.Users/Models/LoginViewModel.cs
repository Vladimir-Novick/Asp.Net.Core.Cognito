using System.ComponentModel.DataAnnotations;

namespace Asp.Net.Core.Cognito.Users.Models
{
    public class LoginViewModel
    {
        [Required]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
