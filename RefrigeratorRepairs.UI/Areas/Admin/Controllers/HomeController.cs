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

        
    }
}
