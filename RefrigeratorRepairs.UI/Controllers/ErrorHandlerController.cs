using Microsoft.AspNetCore.Mvc;

namespace RefrigeratorRepairs.UI.Controllers
{
    public class ErrorHandlerController : Controller
    {
        #region (Methods)
        [Route("/ErrorHandler/{Code}")]
        public IActionResult Index(int Code)
        {
            switch (Code)
            {
                case 404:
                    return View("NotFound");
                case 500:
                    return View("ServerError");
            }
            return View("NotFound");
        }
        #endregion
    }
}
