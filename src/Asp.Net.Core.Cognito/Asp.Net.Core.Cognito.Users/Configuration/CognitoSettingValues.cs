using Amazon;

namespace Asp.Net.Core.Cognito.Users.Configuration
{
    public class CognitoSettingValues
    {
        public string UserPoolId { get; set; }
        public string UserPoolClientId { get; set; }

        public string Region { get; set; }

        public string UserPoolClientSecret { get; set; }
        public RegionEndpoint RegionEndpoint { get
            {
                return RegionEndpoint.GetBySystemName(Region);

            }
        }
    }
}
