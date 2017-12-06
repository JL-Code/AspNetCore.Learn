using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Cookie.Authentication.Models;
using System.Security.Claims;
using AspNetCore.Cookie.Authentication.Entities;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Cookie.Authentication.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var authenticated = User?.Identity?.IsAuthenticated;
            if (authenticated.GetValueOrDefault())
            {
                var userName = User.Identity.Name;
                var cidentity = User.Identity as ClaimsIdentity;
                var dict = cidentity.Claims.ToDictionary(p => p.Type.ToLower(), c => c.Value);
                var identity = new User
                {
                    Name = dict.ContainsKey("Name") ? dict["Name"] : ""
                };
                ViewBag.UserName = userName;
            }

            return View();
        }

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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
