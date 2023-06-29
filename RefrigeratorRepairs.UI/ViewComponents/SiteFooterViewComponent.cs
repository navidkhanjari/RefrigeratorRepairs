using Microsoft.AspNetCore.Mvc;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.UI.ViewModels.SiteSetting;
using System.Linq;

namespace RefrigeratorRepairs.UI.ViewComponents
{
    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly RRContext _DbContext;
        public SiteFooterViewComponent(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        public IViewComponentResult Invoke()
        {
            var model = _DbContext.SiteSettings.FirstOrDefault();

            SiteSettingDetailViewModel SiteSettingDetailViewModel = new SiteSettingDetailViewModel()
            {
                PhoneNumber = model.PhoneNumber,
                Description = model.Description,
            };

            return View("SiteFooter",SiteSettingDetailViewModel);
        }
    }
}
