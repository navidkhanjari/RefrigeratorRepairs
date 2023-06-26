using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorRepairs.UI.Utilities
{
    public class FilePath
    {
        #region Article Images

        public static readonly string ArticlePath = "/images/article/origin/";
        public static readonly string ArticleUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/article/origin/");
        
        public static readonly string ArticleThumbPath = "/images/article/thumb/";
        public static readonly string ArticleThumbUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/article/thumb/");

        public static readonly string ArticleContentImage= "/images/article/content/";
        public static readonly string ArticleContentImageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/article/content/");

        public static string GetArticleImage(string imageName) => $"{ArticlePath.Replace("wwwroot", "")}{imageName}";
        public static string UploadImagePathServer { get; set; }

        #endregion

        #region Site Setting
        public static readonly string LogoImagePath = "/images/setting/logo/";
        public static readonly string LogoImageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/setting/logo/");
       
        public static readonly string BackgroundImagePath = "/images/setting/background/origin/";
        public static readonly string BackgroundImageUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/setting/background/origin/");
        public static readonly string BackgroundThumbImagePath = "/images/setting/background/thumb/";
        public static readonly string BackgroundImageThumbUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/setting/background/thumb/");


        #endregion

        #region MyImages Ckeditor
        public static readonly string MyImagesPath = "/MyImages/";
        public static readonly string MyImagesUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MyImages/");

        #endregion
    }
}
