using Amazon.CognitoIdentityProvider.Model;
using Asp.Net.Core.Cognito.Users;
using Asp.Net.Core.Cognito.Users.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Access
    {
        [TestMethod]
        public void Login()
        {
            var token = (new CognitoAuthentication()).Login("admin", "!QAZxsw2");
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void TokenVerify()
        {
            try
            {
                var token = (new CognitoAuthentication()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var validDate = tokenObject.GetValidDate();
                Assert.IsNotNull(token);
            } catch ( Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void GetPayloads()
        {
            try
            {
                var token = (new CognitoAuthentication()).Login("admin", "!QAZxsw2");
                Assert.IsNotNull(token, "Not Valid token");
                var tokenObject = new JwtTokenObject(token);
                var pailoads = tokenObject.GetPayloads();
                Assert.IsNotNull(pailoads);
                foreach ( var item in pailoads)
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
        public void GetClaims()
        {
            try
            {
                var token = (new CognitoAuthentication()).Login("admin", "!QAZxsw2");
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
                var token = (new CognitoAuthentication()).Login("admin", "!QAZxsw2");
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
        public void UpdateClaim()
        {
            try
            {
                List<AttributeType> attributes = new List<AttributeType>()
                {
                    new AttributeType(){ Name = "phone_number",
                    Value = "+912544497112"
                    }
                };
                (new CognitoAuthentication()).UpdateClaim("admin", "!QAZxsw2", attributes);
                var token = (new CognitoAuthentication()).Login("admin", "!QAZxsw2");
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








    }
}
