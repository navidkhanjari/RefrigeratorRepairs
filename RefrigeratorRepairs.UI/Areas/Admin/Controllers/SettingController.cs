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
        #region (GET)
        [HttpGet("Admin/Settings")]
        public IActionResult SettingIndex()
        {
            var Setting = _DbContext.SiteSettings.FirstOrDefault();

            if (Setting == null)
            {
                #region (Setting Not Found)
                ErrorAlert("خطایی رخ داد!");

                return RedirectToAction("IndexAdmin");
                #endregion
            }

            #region (Fill Fields)
            SiteSettingDetailViewModel SiteSettingDetailViewModel = new SiteSettingDetailViewModel()
            {
                BackgrondImageName = Setting.Background,
                Description = Setting.Description,
                PhoneNumber = Setting.PhoneNumber,
                TextInBackground = Setting.TextInBackground,
                WhatWeDo = Setting.WhatWeDo,
                AboutUs = Setting.AboutUs,
                Id = Setting.Id
            };
            #endregion

            return View(SiteSettingDetailViewModel);
        }
        #endregion
        #endregion

        #region (Edit)
        #region (GET)
        [HttpGet("Admin/Setting/Edit/{Id}")]
        public IActionResult EditSetting(int Id)
        {
            var Setting = _DbContext.SiteSettings.Where(s => s.Id == Id).SingleOrDefault();

            if (Setting == null)
            {
                #region (Setting Not Found)
                ErrorAlert("خطایی رخ داد !");

                return Redirect("/");
                #endregion
            }

            #region (Fill Fields)
            EditSettingViewModel EditSettingViewModel = new EditSettingViewModel()
            {
                Description = Setting.Description,
                PhoneNumber = Setting.PhoneNumber,
                TextInBackground = Setting.TextInBackground,
                BackgroundImageName = Setting.Background,
                AboutUs = Setting.AboutUs,
                WhatWeDo = Setting.WhatWeDo,
                Id = Setting.Id
            };
            #endregion

            return View(EditSettingViewModel);
        }
        #endregion

        #region (POST)
        [HttpPost("Admin/Setting/Edit/{Id}")]
        public IActionResult EditSetting(EditSettingViewModel EditSettingViewModel)
        {
            var Setting = _DbContext.SiteSettings.Where(s => s.Id == EditSettingViewModel.Id).SingleOrDefault();

            if (Setting == null)
            {
                #region (Setting Not Found)
                ErrorAlert("خطایی رخ داد!");

                return Redirect("/");
                #endregion
            }
            try
            {
                #region (Image Null)
                if (EditSettingViewModel.BackgrondImage == null)
                {
                    Setting.TextInBackground = EditSettingViewModel.TextInBackground;
                    Setting.PhoneNumber = EditSettingViewModel.PhoneNumber;
                    Setting.WhatWeDo = EditSettingViewModel.WhatWeDo;
                    Setting.AboutUs = EditSettingViewModel.AboutUs;
                    Setting.Description = EditSettingViewModel.Description;
                }
                #endregion

                #region (Fill Fields)
                #region (Delete Old Image)
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.BackgroundImageUploadPath, Setting.Background);
                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.BackgroundImageUploadPath, Setting.Background);

                if (System.IO.File.Exists(imagePath) && System.IO.File.Exists(imageThumbPath))
                {
                    System.IO.File.Delete(imageThumbPath);
                    System.IO.File.Delete(imagePath);
                }
                #endregion

                Setting.TextInBackground = EditSettingViewModel.TextInBackground;
                Setting.PhoneNumber = EditSettingViewModel.PhoneNumber;
                Setting.Description = EditSettingViewModel.Description;
                Setting.WhatWeDo = EditSettingViewModel.WhatWeDo;
                Setting.AboutUs = EditSettingViewModel.AboutUs;

                #region (Create New Image)
                Setting.Background = Guid.NewGuid().ToString("N") + Path.GetExtension(EditSettingViewModel.BackgrondImage.FileName);
                EditSettingViewModel.BackgrondImage.AddImageToServer(Setting.Background, FilePath.BackgroundImageUploadPath, 150, 150, FilePath.BackgroundImageThumbUploadPath);
                #endregion
                #endregion

                #region (Save)
                _DbContext.SiteSettings.Update(Setting);

                _DbContext.SaveChanges();
                #endregion
            }
            catch
            {
                #region (Error)
                ErrorAlert("عملیات با شکست مواجه شد!");

                return RedirectToAction("SettingIndex");
                #endregion
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
