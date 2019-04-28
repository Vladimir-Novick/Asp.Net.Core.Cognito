using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.Net.Core.Cognito.Models;
using Microsoft.AspNetCore.Authorization;

namespace Asp.Net.Core.Cognito.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Produces("application/json")]
        [Route("api/Token")]
        [Authorize]
        public class TokenController : Controller
        {
            // GET: api/Token
            [HttpGet]
            public string Get()
            {
                string exp = User.Claims.Where(c => c.Type == "exp").First().Value;
                return "Your token is valid until " + (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(Double.Parse(exp)).ToString();
            }
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
