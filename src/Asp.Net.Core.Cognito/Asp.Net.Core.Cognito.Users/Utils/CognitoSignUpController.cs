using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Asp.Net.Core.Cognito.Users.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Asp.Net.Core.Cognito.Users.Utils
{
    public class CognitoSignUpController
    {
        private readonly IAmazonCognitoIdentityProvider _amazonCognitoIdentityProvider;

        public CognitoSignUpController(IAmazonCognitoIdentityProvider amazonCognitoIdentityProvider)
        {
            _amazonCognitoIdentityProvider = amazonCognitoIdentityProvider;
        }

        public async Task<bool> SignUpAsync(string userName, string password, string email)
        {
            try
            {
                var request = CreateSignUpRequest(userName, password, email);
                var authResp = await _amazonCognitoIdentityProvider.SignUpAsync(request);

                return true;
            }
            catch ( Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static SignUpRequest CreateSignUpRequest(string userName, string password, string email)
        {
            var clientId = CognitoSettings.Values.UserPoolClientId ;
            var clientSecretId = CognitoSettings.Values.UserPoolClientSecret ;
            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = email
            };


            List<AttributeType> userAttributesList = new List<AttributeType>();
            userAttributesList.Add(emailAttribute);


            //        List<AttributeType> validationDataList = new List<AttributeType>();
            //        validationDataList.Add(emailValidAttribute);

            var request = new SignUpRequest
            {
                ClientId = clientId,
                SecretHash = CognitoHashCalculator.GetSecretHash(userName, clientId, clientSecretId),
                Username = userName,
                Password = password,
                UserAttributes = userAttributesList

            };


            request.UserAttributes.Add(emailAttribute);
            return request;
        }


    }
}
