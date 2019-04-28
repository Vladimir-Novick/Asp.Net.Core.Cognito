using System;

namespace Asp.Net.Core.Cognito.Users.Excepotions
{
    public class TokenNotValid : Exception
    {
        private string stackTrace;

        public TokenNotValid()
        {
        }

        public TokenNotValid(string message) : base(message)
        {
        }

        public TokenNotValid(string message, string stackTrace) : this(message)
        {
            this.stackTrace = stackTrace;
        }
    }
}
