using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Asp.Net.Core.Cognito.Controllers;
using Asp.Net.Core.Cognito.Models;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using System;

using Amazon;
using Asp.Net.Core.Cognito.Users.Configuration;
using Asp.Net.Core.Cognito.Users.Models;
using Asp.Net.Core.Cognito.Users;
using Asp.Net.Core.Cognito.Users.Validator;

namespace bpcMatchUI.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {

        private readonly ILogger _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }




        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
               

                 var AuthenticationResult = (new CognitoAuthentication()).Authentication(model);

                if (AuthenticationResult == null)
                {
                    ModelState.AddModelError("", "username or password is invalid");
                    return View(model);
                }


                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, AuthenticationResult.IdToken));
                identity.AddClaim(new Claim(ClaimTypes.Name, model.User));

                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true }).GetAwaiter().GetResult();

                return RedirectToLocal(returnUrl);
            }

            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            var token = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
