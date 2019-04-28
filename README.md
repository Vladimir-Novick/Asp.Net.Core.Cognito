# Asp.Net.Core.Cognito

Authentication in ASP.NET Core Web API with Amazon Cognito

Amazon Cognito is the user management and authentication product in AWS. 
This is example how the Amazon Cognito can be integrated into your ASP.NET.CORE application. 

### Verifying a JSON Web Token 

Using NUGET package: System.IdentityModel.Tokens.Jwt

	   var jwtToken = new JwtSecurityToken(token);
       	   DateTime validDateTime = jwtToken.ValidTo;	
       
### Get Paiload info by name 
	   var jwtToken = new JwtSecurityToken(token);
	   var value =  jwtToken.Payload["userInfo"].ToString();
	   
	   
### Get all Claim information (c#)

         var jwtToken = new JwtSecurityToken(token);
         List<ClaimItem> items = new List<ClaimItem>();
         foreach (var claim in jwtToken.Claims)
            {
                ClaimItem item = new ClaimItem()
                {
                    Key = claim.Type,
                    Value = claim.Value
                };
                items.Add(item);
            }
	    
 [Authentication system please look HERE](src/Asp.Net.Core.Cognito/Asp.Net.Core.Cognito.Users/CognitoAuthentication.cs)	    

Copyright (C) 2019 by Vladimir Novick http://www.linkedin.com/in/vladimirnovick , 

vlad.novick@gmail.com , https://github.com/Vladimir-Novick
		 
# License
		 
		 Copyright (C) 2019 by Vladimir Novick http://www.linkedin.com/in/vladimirnovick

		Permission is hereby granted, free of charge, to any person obtaining a copy
		of this software and associated documentation files (the "Software"), to deal
		in the Software without restriction, including without limitation the rights
		to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
		copies of the Software, and to permit persons to whom the Software is
		furnished to do so, subject to the following conditions:

		The above copyright notice and this permission notice shall be included in
		all copies or substantial portions of the Software.

		THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
		IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
		FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
		AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
		LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
		OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
		THE SOFTWARE. 

