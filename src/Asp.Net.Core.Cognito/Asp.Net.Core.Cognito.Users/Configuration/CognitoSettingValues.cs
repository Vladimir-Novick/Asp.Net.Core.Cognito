namespace Asp.Net.Core.Cognito.Users.Configuration
{
    public class CognitoSettingValues
    {
        public string poolID { get; set; }
        public string clientID { get; set; }

        public string clientSecret { get; set; }

        public string awsAccessKeyId { get; set; }

        public string awsSecretAccessKey { get; set; }
    }
}
