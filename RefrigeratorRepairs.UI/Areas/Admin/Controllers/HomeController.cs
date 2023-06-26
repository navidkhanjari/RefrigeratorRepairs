using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefrigeratorRepairs.MODEL.Context;

namespace RefrigeratorRepairs.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        #region (Costructor)
        private readonly RRContext _DbContext;
        public HomeController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion

        #region (Index)
        [HttpGet("AdminPanel")]
        public IActionResult IndexAdmin()
        {
            return View("Index");
        }
        #endregion
    }
}
