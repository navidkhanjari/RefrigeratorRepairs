using Microsoft.AspNetCore.Mvc;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.UI.ViewModels.SiteSetting;
using System.Linq;

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

    }
}
