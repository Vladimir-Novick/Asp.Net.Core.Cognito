using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Asp.Net.Core.Cognito.Users.Excepotions;
using Asp.Net.Core.Cognito.Users.Models;
using System.Collections.Generic;

namespace Asp.Net.Core.Cognito.Users.Validator
{
    public class JwtTokenObject
    {

        private JwtSecurityToken jwtToken = null;

        public JwtTokenObject(String token)
        {
            try
            {
                jwtToken = new JwtSecurityToken(token);
                if (jwtToken == null)
                {
                    throw new TokenNotValid();
                }
            } catch ( Exception ex)
            {
                throw new TokenNotValid(ex.Message,ex.StackTrace);
            }
        }

        public DateTime GetValidDate()
        {
            DateTime validDateTime = jwtToken.ValidTo;
            return validDateTime;
        }


        public String GetPayloadItem(string key)
        {
            var item = jwtToken.Payload[key].ToString();
            return item;
        }

        public List<PayloadItem> GetPayloads()
        {
            List<PayloadItem> items = new List<PayloadItem>();

            foreach (var paiload in jwtToken.Payload)
            {
                PayloadItem item = new PayloadItem()
                {
                    Key = paiload.Key,
                    Value = paiload.Value.ToString()
                };
                items.Add(item);
            }

            return items;
        }

        public List<PayloadItem> GetClaims()
        {
            List<PayloadItem> items = new List<PayloadItem>();

            foreach (var claim in jwtToken.Claims)
            {
                PayloadItem item = new PayloadItem()
                {
                    Key = claim.Type,
                    Value = claim.Value
                };
                items.Add(item);
            }

            return items;
        }

        public static string VerifyJWT(string token)
        {

            var jwtToken = new JwtSecurityToken(token);

            return String.Empty;
        }

    }
}
