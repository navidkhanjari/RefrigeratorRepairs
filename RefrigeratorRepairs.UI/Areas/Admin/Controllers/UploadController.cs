using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefrigeratorRepairs.UI.Utilities;

namespace RefrigeratorRepairs.UI.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        [Route("/Upload/Article")]
        public IActionResult Article(IFormFile upload)
        {
            if(upload == null)
            {
                return BadRequest();
            }

            var imageName = UploadImage.SaveFileAndReturnName(upload, FilePath.ArticleContentImageUploadPath);
            var url = FilePath.ArticleContentImage;

            return Json(new { Uploaded = true, url = FilePath.ArticleContentImage + imageName });
        }
    }
}
