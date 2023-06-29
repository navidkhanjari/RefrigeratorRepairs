using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.MODEL.Entities.User;
using RefrigeratorRepairs.UI.Utilities;
using RefrigeratorRepairs.UI.ViewModels.Account;
using RefrigeratorRepairs.UI.ViewModels.Articles;
using RefrigeratorRepairs.UI.ViewModels.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RefrigeratorRepairs.UI.Controllers
{
    public class HomeController : Controller
    {
        #region (Constructor)
        private readonly RRContext _DbContext;
        public HomeController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion

        #region (Methods)
        #region (Index)
        public IActionResult Index()
        {
            var SiteSetting = _DbContext.SiteSettings.FirstOrDefault();

            SiteSettingDetailViewModel SiteSettingDetailViewModel = new SiteSettingDetailViewModel()
            {
                TextInBackground = SiteSetting.TextInBackground,
                BackgrondImageName = SiteSetting.Background,
                Description = SiteSetting.Description,
                PhoneNumber = SiteSetting.PhoneNumber,
                AboutUs = SiteSetting.AboutUs,
                WhatWeDo = SiteSetting.WhatWeDo,
            };

            return View(SiteSettingDetailViewModel);
        }
        #endregion

        #region (About Us)
        #region (GET)
        [HttpGet("ContactUs")]
        public IActionResult ContactUs()
        {
            var Setting = _DbContext.SiteSettings.FirstOrDefault();

            if (Setting == null)
            {
                #region (NotFound)
                ErrorAlert("خطایی رخ داد!");

                return Redirect("/");
                #endregion
            }

            #region (Fill Fields)
            SiteSettingContactUs SiteSettingContactUs = new SiteSettingContactUs()
            {
                AboutUs = Setting.AboutUs,
            };
            #endregion

            return View(SiteSettingContactUs);
        }
        #endregion
        #endregion

        #region (Articles)
        #region (Show All)
        [HttpGet("Articles")]
        public IActionResult ShowAll(ArticleFilterViewModel ArticleFilterViewModel)
        {
            return View(this.FilterArticleAsync(ArticleFilterViewModel));
        }
        #endregion

        #region (Single Page)
        #region (GET)
        [HttpGet("Article/{Slug}")]
        public IActionResult SingleArticle(string Slug)
        {
            var Article = _DbContext.Articles.Where(A => A.Slug == Slug).SingleOrDefault();

            if (Article == null)
            {
                #region (Article Not Found)
                ErrorAlert("مقاله یافت نشد!");

                return RedirectToAction("Index");
                #endregion
            }

            Article.Visit += 1;

            ViewData["SiteUrl"] = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            return View(Article);
        }
        #endregion
        #endregion
        #endregion

        #region (Admin Login)
        #region (Get)
        [HttpGet("Ad/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }
        #endregion

        #region (Post)
        [HttpPost("Ad/Login")]
        public IActionResult Login(LoginViewModel LoginViewModel)
        {
            var Admin = _DbContext.Users.Where(U => U.UserName == LoginViewModel.UserName && U.Role == Role.Admin).SingleOrDefault();

            if (Admin == null)
            {
                #region (Not Found)
                ErrorAlert("کاربری یافت نشد!");

                return Redirect("/");
                #endregion
            }

            #region (Authentication)
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,Admin.FirstName),
            };

            var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Principal = new ClaimsPrincipal(Identity);
            var Properties = new AuthenticationProperties
            {
                IsPersistent = LoginViewModel.RememberMe,
            };

            HttpContext.SignInAsync(Principal, Properties);
            #endregion

            return Redirect("/AdminPannel");
        }
        #endregion
        #endregion

        #region (Sweet Alerts)
        protected void SuccessAlert()
        {
            var Model = JsonConvert.SerializeObject(JsonAlertType.Success());

            HttpContext.Response.Cookies.Append("SystemAlert", Model);
        }
        protected void SuccessAlert(String Message)
        {
            var Model = JsonConvert.SerializeObject(JsonAlertType.Success(Message));

            HttpContext.Response.Cookies.Append("SystemAlert", Model);
        }

        protected void ErrorAlert()
        {
            var Model = JsonConvert.SerializeObject(JsonAlertType.Error());

            HttpContext.Response.Cookies.Append("SystemAlert", Model);
        }
        protected void ErrorAlert(String Message)
        {
            var Model = JsonConvert.SerializeObject(JsonAlertType.Error(Message));

            HttpContext.Response.Cookies.Append("SystemAlert", Model);
        }
        #endregion

        #region (Filter)
        public ArticleFilterViewModel FilterArticleAsync(ArticleFilterViewModel filter)
        {
            var query = _DbContext.Articles.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(r => EF.Functions.Like(r.Title, $"%{filter.Title}%"));
            }
            #endregion

            #region  Paging
            filter.Build(query.Count()).SetEntities(query);
            #endregion
            return filter;
        }
        #endregion
        #endregion
    }
}
