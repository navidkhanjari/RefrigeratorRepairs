using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.MODEL.Entities.User;
using RefrigeratorRepairs.UI.Utilities;
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
            var model = _DbContext.SiteSettings.FirstOrDefault();
            SiteSettingDetailViewModel SiteSettingDetailViewModel = new SiteSettingDetailViewModel()
            {
                TextInBackground = model.TextInBackground,
                BackgrondImageName = model.Background,
                Description = model.Description,
                PhoneNumber = model.PhoneNumber,
                AboutUs = model.AboutUs,
                WhatWeDo = model.WhatWeDo,
            };

            return View(SiteSettingDetailViewModel);
        }
        #endregion

        #region (About Us)
        [HttpGet("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }
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
        [HttpGet("Article/{Slug}")]
        public IActionResult SingleArticle(string Slug)
        {
            var article = _DbContext.Articles.Where(A => A.Slug == Slug).SingleOrDefault();

            if (article == null)
            {
                ErrorAlert("مقاله یافت نشد!");

                return RedirectToAction("Index");
            }

            article.Visit += 1;

            ViewData["SiteUrl"] = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

            return View(article);
        }
        #endregion
        #endregion

        #region (Admin Login)
        #region (Get)
        [HttpGet("/Ad/Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }
        #endregion

        #region (Post)
        [HttpPost("/Ad/Login")]
        public IActionResult Login(User user)
        {
            var admin = _DbContext.Users.Where(U => U.Id == user.Id && U.Role == Role.Admin).SingleOrDefault();

            if (admin == null)
            {
                ErrorAlert("کاربری یافت نشد!");

                return Redirect("/");
            }

            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.FirstName),
            };

            var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var Principal = new ClaimsPrincipal(Identity);
            var Properties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(Principal, Properties);

            return Redirect("/Admin");
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
