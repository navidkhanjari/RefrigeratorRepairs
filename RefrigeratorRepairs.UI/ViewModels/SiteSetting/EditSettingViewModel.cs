using Microsoft.AspNetCore.Http;

namespace RefrigeratorRepairs.UI.ViewModels.SiteSetting
{
    public class EditSettingViewModel
    {
        public int Id { get; set; }
        public IFormFile BackgrondImage { get; set; }
        public string TextInBackground { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string BackgroundImageName { get; set; }
        public string WhatWeDo { get; set; }
        public string AboutUs { get; set; }
    }
}
