using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.UI.ViewModels.SiteSetting
{
    public class EditSettingViewModel
    {
        public int Id { get; set; }
        
        public IFormFile BackgrondImage { get; set; }
        
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string TextInBackground { get; set; }
        
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string BackgroundImageName { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string WhatWeDo { get; set; }

        [UIHint("Ckeditor4")]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        [Display(Name ="درباره ما")]
        public string AboutUs { get; set; }
    }
}
