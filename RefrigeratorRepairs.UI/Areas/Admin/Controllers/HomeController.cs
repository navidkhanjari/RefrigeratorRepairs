using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.MODEL.Entities.User;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RefrigeratorRepairs.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        #region (Costructor)
        private readonly RRContext _DbContext;
        public HomeController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet("/Administrator/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        [HttpPost("/Administrator/Login")]
        public IActionResult Login(User user)
        {
            var admin = _DbContext.Users.Where(U => U.Id == user.Id).SingleOrDefault();

            if (admin == null)
            {
                //Alert Not Found

                //return To home page
            }

            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,user.FirstName),
                        };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true
            };

             HttpContext.SignInAsync(principal, properties);
            return Redirect("/Admin");
        }
    }
}
