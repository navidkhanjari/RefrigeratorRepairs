using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace RefrigeratorRepairs.UI.Utilities
{
    public static class UploadImage
    {
        public static void AddImageToServer(this IFormFile image, string fileName, string orginalPath, int? width, int? height, string thumbPath = null, string deletefileName = null)
        {
            if (image != null && image.IsImage())
            {
                if (!Directory.Exists(orginalPath))
                    Directory.CreateDirectory(orginalPath);

                if (!string.IsNullOrEmpty(deletefileName))
                {
                    if (File.Exists(orginalPath + deletefileName))
                        File.Delete(orginalPath + deletefileName);

                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        if (File.Exists(thumbPath + deletefileName))
                            File.Delete(thumbPath + deletefileName);
                    }
                }

                string OriginPath = orginalPath + fileName;

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath)) image.CopyTo(stream);
                }


                if (!string.IsNullOrEmpty(thumbPath))
                {
                    if (!Directory.Exists(thumbPath))
                        Directory.CreateDirectory(thumbPath);

                    ImageOptimizer resizer = new ImageOptimizer();

                    if (width != null && height != null)
                        resizer.ImageResizer(orginalPath + fileName, thumbPath + fileName, width, height);
                }
            }
        }
        public static string SaveFileAndReturnName(IFormFile file, string savepath)
        {
            if (file == null)
                throw new Exception("File Is Null");

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);


            file.AddImageToServer(fileName, savepath, 200, 200);

            return fileName;
        }
    }
}
