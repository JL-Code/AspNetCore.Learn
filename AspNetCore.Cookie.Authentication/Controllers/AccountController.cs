using Microsoft.AspNetCore.Mvc;
using AspNetCore.Cookie.Authentication.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.Cookie.Authentication.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var userStore = HttpContext.RequestServices.GetService<UserStore>();
            var user = userStore.FindUser(username, password);
            if (user == null)
            {
                ViewBag.ErrMsg = "用户名或密码错误";
                return View();
            }
            else
            {
                //制作身份证
                var claimIdentity = new ClaimsIdentity("Cookies");
                //往身份证上添加身份信息
                claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                claimIdentity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));
                claimIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString()));
                //生成身份持有人
                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                //登录
                HttpContext.SignInAsync(claimsPrincipal);
                //重定向到首页
                return RedirectToAction("index", "home");
            }
        }
    }
}
