using System;
using System.IdentityModel.Tokens.Jwt;
using Asp.Net.Core.Cognito.Users.Excepotions;
using Asp.Net.Core.Cognito.Users.Models;
using System.Collections.Generic;
using System.Linq;

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
        /// <summary>
        ///     Gets the 'expiration' datetime converted to a System.DateTime,  UnixEpoch (UTC 1970-01-01T0:0:0Z). 
        /// </summary>
        /// <returns></returns>
        public DateTime GetValidDate()
        {
             return jwtToken.ValidTo;
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

        public bool IsValidDate()
        {
            var validDate = GetValidDate();
            if (validDate > DateTime.Now) return false;
            return true;
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

        public Dictionary<string,string> GetProperties()
        {
          var items = new Dictionary<String, String>();
            return jwtToken.Claims.ToDictionary(x => x.Type, x => x.Value);

        }


    }
}
