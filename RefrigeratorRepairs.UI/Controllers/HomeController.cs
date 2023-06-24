using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.MODEL.Entities.User;
using RefrigeratorRepairs.UI.ViewModels.SiteSetting;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RefrigeratorRepairs.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly RRContext _DbContext;

        public HomeController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }

        public IActionResult Index()
        {
            var model = _DbContext.SiteSettings.FirstOrDefault();
            SiteSettingDetailViewModel SiteSettingDetailViewModel = new SiteSettingDetailViewModel()
            {
                TextInBackground = model.TextInBackground,
                BackgrondImageName = model.Background,
                Description = model.Description,
                PhoneNumber = model.PhoneNumber
            };

            return View(SiteSettingDetailViewModel);
        }

        [HttpGet("/Ad/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }

        [HttpPost("/Ad/Login")]
        public IActionResult Login(User user)
        {
            var admin = _DbContext.Users.Where(U => U.Id == user.Id && U.Role == Role.Admin).SingleOrDefault();

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
