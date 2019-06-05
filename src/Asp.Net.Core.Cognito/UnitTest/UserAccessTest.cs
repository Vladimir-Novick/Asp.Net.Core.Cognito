using Amazon.AspNetCore.Identity.Cognito.Exceptions;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Asp.Net.Core.Cognito.Users;
using Asp.Net.Core.Cognito.Users.Configuration;
using Asp.Net.Core.Cognito.Users.Models;
using Asp.Net.Core.Cognito.Users.Validator;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UserAccessTest
    {

        public UserAccessTest()
        {
            CognitoSettings.AddJsonFile("/../../../../Asp.Net.Core.Cognito/config/cognito_config.json");
        }

        [TestMethod]
        public void Login()
        {
            var token = (new CognitoUserManager()).Login("admin", "!QAZxsw2");
            Assert.IsNotNull(token);
            var tokenObject = new JwtTokenObject(token);
            var jwt = new JwtSecurityToken(token);
        }




        [TestMethod]
        public void Register()
        {
            var response = (new CognitoUserManager()).Register(new ApiRegisterRequest()
            {
                UserName = "ApiRegisterTest_" + Guid.NewGuid().ToString().Substring(0, 8),
                Password = "!QAZxsw2",
                eMail = "vlad.novick@gmail.com",
                Attributes = new Dictionary<string, string>()
               {
                   {"custom:dep_id","123"}
               }
            });
            Assert.IsTrue(response);

        }


        [TestMethod]
        public void ChangePassword()
        {
            try
            {
                var ok = (new CognitoUserManager()).ChangePassword(new ApiChangePasswordRequest()
                {
                    UserName = "vlad",
                    PreviewPassword = "!QAZxsw2",
                    NewPassword = "!QAZxsw2"
                });
                Assert.IsTrue(ok, "Password not changed");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void AdminSetPassword()
        {
            try
            {
                CognitoUserManager cognito = new CognitoUserManager();
                var token = cognito.Login("admin", "!QAZxsw2");  // login as admin


                var ok = cognito.AdminSetPassword(new ApiAdminSetPasswordRequest()
                {
                    UserName = "vlad",
                    Password = "!QAZxsw3"
                   
                });

                Assert.IsTrue(ok, "Password not changed");

                var token2 = cognito.Login("vlad", "!QAZxsw3");
                Assert.IsNotNull(token2, "Password not changed");

                var token3 = cognito.Login("admin", "!QAZxsw2");  // login as admin

                ok = cognito.AdminSetPassword(new ApiAdminSetPasswordRequest()
                {
                    UserName = "vlad",
                    Password = "!QAZxsw2"

                });

                Assert.IsTrue(ok, "Password not changed");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void logout()
        {
            CognitoUserManager cognito = new CognitoUserManager();
            var token = cognito.Login("admin", "!QAZxsw2");
            cognito.Logout();
            Assert.IsNotNull(token);
        }


        [TestMethod]
        public void GetValidDate()
        {
            try
            {
                var token = (new CognitoUserManager()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var validDate = tokenObject.GetValidDate().ToUniversalTime();

                Assert.IsNotNull(validDate);

                DateTime utcDate = DateTime.Now.ToUniversalTime();
                var time = tokenObject.GetValidDate() - utcDate;

                if (time.TotalSeconds < 0)
                {
                    Console.WriteLine($"different msc {time.TotalSeconds}");
                }
                string textDate = String.Format("{0:dd.MM.yyyy HH:mm:ss}", tokenObject.GetValidDate());
                Console.WriteLine($"Token DateTime UTC: {textDate}");
                String strDatetimeNow = String.Format("{0:dd.MM.yyyy HH:mm:ss}", utcDate);
                Console.WriteLine($"Now DateTime UTC: {strDatetimeNow}");



            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void IsValidDate()
        {
            try
            {
                var token = (new CognitoUserManager()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var validDate = tokenObject.IsValidDate();

                Assert.IsTrue(validDate);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }




        [TestMethod]
        public void GetPayloads()
        {
            try
            {
                var token = (new CognitoUserManager()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var pailoads = tokenObject.GetPayloads();
                Assert.IsNotNull(pailoads);
                foreach (var item in pailoads)
                {
                    Console.WriteLine($" Key: {item.Key} , Value : {item.Value}");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void GetTokenProperties()
        {
            try
            {
                var token = (new CognitoUserManager()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var pailoads = tokenObject.GetClaims();
                Assert.IsNotNull(pailoads);
                foreach (var item in pailoads)
                {
                    Console.WriteLine($" Key: {item.Key} , Value : {item.Value}");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void GetSinglePayload()
        {
            try
            {
                var token = (new CognitoUserManager()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var value = tokenObject.GetPayloadItem("phone_number");
                Assert.IsNotNull(value);

                Console.WriteLine($" Key: phone_number , Value : {value}");

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void FindUser()
        {
            var loginUser = new CognitoUserManager();
            loginUser.Login("admin", "!QAZxsw2");
            var user = loginUser.FindUser("vlad_api");
           

        }


        [TestMethod]
        public void DeleteUser()
        {
            var loginUser = new CognitoUserManager();
            loginUser.Login("admin", "!QAZxsw2");
            var user = loginUser.DeleteUser("vlad_api2");


        }




        [TestMethod]
        public void UpdateAttributes()
        {
            try
            {

                var loginUser = new CognitoUserManager();

                loginUser.Login("admin", "!QAZxsw2");

                var updateValue = new ApiUpdateRequest()
                {
                    UserName = "vlad",
                    Attributes = new Dictionary<string, string>()
                     {
                         {"phone_number","+912544497111"}
                     }
                };


                var ok = loginUser.UpdateAttributes(updateValue);

                Assert.IsTrue(ok);



            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
}



[TestMethod]
public void GetUserList()
{
    var loginUser = new CognitoUserManager();
    loginUser.Login("admin", "!QAZxsw2");

    var ret = loginUser.GetUserList();

    string jsonData = JsonConvert.SerializeObject(ret);
    Console.WriteLine(jsonData);

}





    }
}
