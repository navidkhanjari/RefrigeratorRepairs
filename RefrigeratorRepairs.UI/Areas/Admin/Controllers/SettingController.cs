using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.UI.Utilities;
using RefrigeratorRepairs.UI.ViewModels.SiteSetting;
using System;
using System.IO;
using System.Linq;

namespace RefrigeratorRepairs.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class SettingController : Controller
    {
        #region (Constructor)
        private readonly RRContext _DbContext;
        public SettingController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion

        #region (Methods)
        #region (Index)
        [HttpGet("Admin/Settings")]
        public IActionResult SettingIndex()
        {
            var setting = _DbContext.SiteSettings.FirstOrDefault();

            if(setting == null)
            {
                #region (Setting Not Found)
                ErrorAlert("خطایی رخ داد!");

                return RedirectToAction("IndexAdmin");
                #endregion
            }
            
            #region (Fill Fields)
            SiteSettingDetailViewModel model = new SiteSettingDetailViewModel()
            {
                BackgrondImageName = setting.Background,
                Description = setting.Description,
                PhoneNumber = setting.PhoneNumber,
                TextInBackground = setting.TextInBackground,
                WhatWeDo = setting.WhatWeDo,
                AboutUs = setting.AboutUs,
                Id = setting.Id
            };
            #endregion

            return View(model);
        }
        #endregion

        #region (Edit)
        #region (GET)
        [HttpGet("Admin/Setting/Edit/{Id}")]
        public IActionResult EditSetting(int Id)
        {
            var setting = _DbContext.SiteSettings.Where(s => s.Id == Id).SingleOrDefault();
            if (setting == null)
            {
                ErrorAlert("خطایی رخ داد !");

                return Redirect("/");
            }
            EditSettingViewModel model = new EditSettingViewModel()
            {
                Description = setting.Description,
                PhoneNumber = setting.PhoneNumber,
                TextInBackground = setting.TextInBackground,
                BackgroundImageName = setting.Background,
                AboutUs = setting.AboutUs,
                WhatWeDo =setting.WhatWeDo,
                Id = setting.Id
            };

            return View(model);
        }
        #endregion

        #region (POST)
        [HttpPost("Admin/Setting/Edit/{Id}")]
        public IActionResult EditSetting(EditSettingViewModel EditSettingViewModel)
        {
            var setting = _DbContext.SiteSettings.Where(s => s.Id == EditSettingViewModel.Id).SingleOrDefault();
            if (setting == null)
            {
                ErrorAlert("خطایی رخ داد!");

                return Redirect("/");
            }
            try
            {
                if (EditSettingViewModel.BackgrondImage == null)
                {
                    setting.TextInBackground = EditSettingViewModel.TextInBackground;
                    setting.PhoneNumber = EditSettingViewModel.PhoneNumber;
                    setting.WhatWeDo = EditSettingViewModel.WhatWeDo;
                    setting.AboutUs = EditSettingViewModel.AboutUs;
                    setting.Description = EditSettingViewModel.Description;
                }

                if (EditSettingViewModel.BackgrondImage != null)
                {
                    // delete old image 
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.BackgroundImageUploadPath, setting.Background);
                    var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.BackgroundImageUploadPath, setting.Background);

                    if (System.IO.File.Exists(imagePath) && System.IO.File.Exists(imageThumbPath))
                    {
                        System.IO.File.Delete(imageThumbPath);
                        System.IO.File.Delete(imagePath);
                    }

                    setting.TextInBackground = EditSettingViewModel.TextInBackground;
                    setting.PhoneNumber = EditSettingViewModel.PhoneNumber;
                    setting.Description = EditSettingViewModel.Description;
                    setting.WhatWeDo = EditSettingViewModel.WhatWeDo;
                    setting.AboutUs = EditSettingViewModel.AboutUs;

                    setting.Background = Guid.NewGuid().ToString("N") + Path.GetExtension(EditSettingViewModel.BackgrondImage.FileName);

                    EditSettingViewModel.BackgrondImage.AddImageToServer(setting.Background, FilePath.BackgroundImageUploadPath, 150, 150, FilePath.BackgroundImageThumbUploadPath);

                }

                _DbContext.Update(setting);
                _DbContext.SaveChanges();
            }
            catch
            {
                ErrorAlert("عملیات با شکست مواجه شد!");

                return RedirectToAction("SettingIndex");
            }

            return RedirectToAction("SettingIndex");
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
        #endregion
    }
}
