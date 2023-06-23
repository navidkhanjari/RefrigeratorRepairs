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

        public static string UploadImagePathServer { get; set; }

        #endregion

        #region MyImages Ckeditor
        public static readonly string MyImagesPath = "/MyImages/";
        public static readonly string MyImagesUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MyImages/");

        #endregion
    }
}
