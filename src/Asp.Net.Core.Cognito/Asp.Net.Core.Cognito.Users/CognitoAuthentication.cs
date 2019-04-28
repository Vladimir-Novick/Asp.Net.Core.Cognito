using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Asp.Net.Core.Cognito.Users.Configuration;
using Asp.Net.Core.Cognito.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asp.Net.Core.Cognito.Users
{
    public class CognitoAuthentication
    {
        private  AmazonCognitoIdentityProviderClient cognitoIdentityProvider()
        {
            String awsAccessKeyId = CognitoSettings.cognitoSettingValues.awsAccessKeyId;
            String awsSecretAccessKey = CognitoSettings.cognitoSettingValues.awsSecretAccessKey;
            RegionEndpoint region = RegionEndpoint.EUWest1;
            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient(awsAccessKeyId, awsSecretAccessKey, region);
            return provider;
        }



        public  AuthenticationResultType Authentication(LoginViewModel _loginModel)
        {
            String userName = _loginModel.User;
            String password = _loginModel.Password;

            AuthenticationResultType status = null;

            status = Authentication(userName, password);

            return status;

        }

        public  string Login(string userName, string password)
        {
            string token = null;
            try
            {
                var authenticationResult = Authentication(userName, password);
                if (authenticationResult != null)
                {
                    token = authenticationResult.IdToken;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                token = null;
            }
            return token;
        }



        public bool UpdateClaim(string userName, string password, List<AttributeType> attributes, bool reconnect = false)
        {
            try
            {
                var updates = attributes.ToDictionary(x => x.Name, x => x.Value);
                GetUser(userName, password, reconnect).UpdateAttributesAsync(updates).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }



        private CognitoUser _user { get; set; } = null;

        private CognitoUser GetUser(String userName, String password, bool reconnect = false) {
            if (_user == null || reconnect)
            {
                Authentication(userName, password);
            }
            return _user;
    }


        private  AuthenticationResultType Authentication(string userName, string password)
        {

            AuthenticationResultType status = null;

            _user = GetCognitoUser(userName);

            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = password
            };

            try
            {
                var authResponse = _user.StartWithSrpAuthAsync(authRequest).GetAwaiter().GetResult();

                if (authResponse != null)
                {
                    Console.WriteLine("User successfully authenticated.");
                    status = authResponse.AuthenticationResult;


                    if (authResponse.ChallengeName == ChallengeNameType.NEW_PASSWORD_REQUIRED)
                    {
                        var result2 = _user.RespondToNewPasswordRequiredAsync(new RespondToNewPasswordRequiredRequest()
                        {
                            SessionID = authResponse.SessionID,
                            NewPassword = password
                        }).ConfigureAwait(false).GetAwaiter().GetResult();

                        status = result2.AuthenticationResult;
                    }



                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                status = null;
            }

            if (status == null)
            {
                _user = null;
            }

            return status;
        }


        private CognitoUser GetCognitoUser(string userName)
        {
            AmazonCognitoIdentityProviderClient userProvider = cognitoIdentityProvider();

            string poolID = CognitoSettings.cognitoSettingValues.poolID;
            string clientID = CognitoSettings.cognitoSettingValues.clientID;
            string clientSecret = CognitoSettings.cognitoSettingValues.clientSecret;

            CognitoUserPool userPool = new CognitoUserPool(poolID,
                clientID, userProvider, clientSecret);
            CognitoUser user = new CognitoUser(userName, clientID, userPool, userProvider, clientSecret, username: userName);

            return user;
        }
    }
}
