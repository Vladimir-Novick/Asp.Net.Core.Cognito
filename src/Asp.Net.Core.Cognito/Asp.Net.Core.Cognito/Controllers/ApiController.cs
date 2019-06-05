using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Asp.Net.Core.Cognito.Users.Models;
using Asp.Net.Core.Cognito.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Asp.Net.Core.Cognito.Users.Utils;
using Amazon.CognitoIdentityProvider.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Amazon.Extensions.CognitoAuthentication;

namespace Asp.Net.Core.Cognito.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/[action]")]
    public class ApiController : Controller
    {

        public ApiController(UserManager<CognitoUser> userManager, SignInManager<CognitoUser> signInManager)
        {

        }

        /// <summary>
        ///  Sign in and return access token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignIn([FromBody] ApiLoginRequest model)
        {
            String result = "";
            if (model == null || ApiUtils.isEmpty(model.UserName) || ApiUtils.isEmpty(model.Password))
            {
                result = "Login Model is faliled";
            }
            else
            {
                CognitoUserManager cognito = new CognitoUserManager();
                var token = cognito.Login(model.UserName, model.Password);
                if (token != null)
                {
                    result = token;
                }
                else
                {
                    result = "Login Error ";
                }
            }

            return Ok(result);
        }



        [Authorize]
        [HttpPost]
        public IActionResult Register([FromBody] ApiRegisterRequest input)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                using (var userManager = new CognitoUserManager())
                {
                    var ret = userManager.Register(input);
                    if (ret) return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new BadRequestResult();
            }

            return response;
        }

        /// <summary>
        ///    Change password for registered user
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword([FromBody] ApiChangePasswordRequest input)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var ret = (new CognitoUserManager()).ChangePassword(input);
                if (ret) return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new BadRequestResult();
            }

            return response;
        }

        /// <summary>
        ///  Reset password. Only for Administrator permissions. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public IActionResult AdminSetPassword([FromBody] ApiAdminSetPasswordRequest input)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var ret = (new CognitoUserManager()).AdminSetPassword(input);
                if (ret) return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new BadRequestResult();
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public IActionResult GetUserList()
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var ret = (new CognitoUserManager()).GetUserList();
                if (ret != null) return Ok(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new BadRequestResult();
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Update([FromBody] ApiUpdateRequest request)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var ret = (new CognitoUserManager()).UpdateAttributes(request);
                if (ret ) return Ok(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new BadRequestResult();
            }

            return response;
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteUser([FromBody] ApiDeleteUserRequest deleteUserRequest)
        {
            IActionResult response = new BadRequestResult();

            try
            {
                var ret = (new CognitoUserManager()).DeleteUser(deleteUserRequest.UserName);
                if (ret) return Ok(ret);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new BadRequestResult();
            }

            return response;
        }



    }
}
