using Microsoft.AspNetCore.Mvc;

namespace RefrigeratorRepairs.UI.ViewComponents
{
    public class SiteHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View("SiteHeader");
        }
    }
}
