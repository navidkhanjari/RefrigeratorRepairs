﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.MODEL.Entities.Settings;
using RefrigeratorRepairs.UI.Utilities;
using RefrigeratorRepairs.UI.ViewModels.SiteSetting;
using System;
using System.IO;
using System.Linq;

namespace RefrigeratorRepairs.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        #region (Constructor)
        private readonly RRContext _DbContext;
        public SettingController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion

        #region (Index)
        [HttpGet("Admin/Settings")]
        public IActionResult SettingIndex()
        {
            var setting = _DbContext.SiteSettings.FirstOrDefault();

            SiteSettingDetailViewModel model = new SiteSettingDetailViewModel()
            {
                BackgrondImageName = setting.Background,
                Description = setting.Description,
                PhoneNumber = setting.PhoneNumber,
                TextInBackground = setting.TextInBackground,
                Id = setting.Id
            };

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
                //return index
            }
            EditSettingViewModel model = new EditSettingViewModel()
            {
                Description = setting.Description,
                PhoneNumber = setting.PhoneNumber,
                TextInBackground = setting.TextInBackground,
                BackgroundImageName = setting.Background,
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
                ErrorAlert("خطایی رخ داد !");
                //return index
            }
            try
            {
                if (EditSettingViewModel.BackgrondImage == null)
                {
                    setting.TextInBackground = EditSettingViewModel.TextInBackground;
                    setting.PhoneNumber = EditSettingViewModel.PhoneNumber;
                    setting.Description = EditSettingViewModel.Description;
                }

                if (EditSettingViewModel.BackgrondImage != null )
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

                    setting.Background = Guid.NewGuid().ToString("N") + Path.GetExtension(EditSettingViewModel.BackgrondImage.FileName);

                    EditSettingViewModel.BackgrondImage.AddImageToServer(setting.Background, FilePath.BackgroundImageUploadPath, 150, 150, FilePath.BackgroundImageThumbUploadPath);

                }

                _DbContext.Update(setting);
                _DbContext.SaveChanges();
            }
            catch
            {

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
    }
}
