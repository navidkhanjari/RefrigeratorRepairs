using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RefrigeratorRepairs.MODEL.Context;
using RefrigeratorRepairs.UI.Utilities;
using RefrigeratorRepairs.UI.ViewModels.Articles;
using System;
using System.IO;
using System.Linq;

namespace RefrigeratorRepairs.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ArticleController : Controller
    {
        #region (Constructor)
        private readonly RRContext _DbContext;
        public ArticleController(RRContext DbContext)
        {
            _DbContext = DbContext;
        }
        #endregion

        #region (Methods)
        #region (Index)
        [HttpGet("Admin/Articles")]
        public IActionResult IndexArticle(ArticleFilterViewModel ArticleFilterViewModel)
        {
            return View(this.FilterArticleAsync(ArticleFilterViewModel));
        }
        #endregion

        #region (Add)
        #region (GET)
        [HttpGet("Admin/Article/Add")]
        public IActionResult AddArticle()
        {
            return View();
        }
        #endregion

        #region (POST)
        [HttpPost("Admin/Article/Add")]
        public IActionResult AddArticle(AddArticleViewModel AddArticleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(AddArticleViewModel);
            }

            var Articles = _DbContext.Articles.Where(A => A.Slug == AddArticleViewModel.Slug).SingleOrDefault();

            if (Articles != null)
            {
                #region (Slug Exist)
                this.ErrorAlert("اسلاگ تکراری است!");

                return View(AddArticleViewModel);
                #endregion
            }

            try
            {
                #region (Fill Fields)
                MODEL.Entities.Article.Article Article = new MODEL.Entities.Article.Article()
                {
                    Title = AddArticleViewModel.Title,
                    Description = AddArticleViewModel.Description,
                    ShortDescription = AddArticleViewModel.ShortDescription,
                    Slug = AddArticleViewModel.Slug.ToSlug(),
                    MetaDescription = AddArticleViewModel.MetaDescription,
                    MetaKeyword = AddArticleViewModel.MetaKeyword,
                    ImageAlt = AddArticleViewModel.ImageAlt,
                    Visit = 0,
                    CreatedDate = DateTime.Now,
                    ImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(AddArticleViewModel.Image.FileName),
                };

                #region (CREATE NEW IMAGE)
                AddArticleViewModel.Image.AddImageToServer(Article.ImageName, FilePath.ArticleUploadPath, 200, 200, FilePath.ArticleThumbUploadPath);
                #endregion
                #endregion

                #region (Save)
                _DbContext.Articles.Add(Article);

                _DbContext.SaveChanges();

                this.SuccessAlert("عملیات با موفقیت انجام شد.");

                return RedirectToAction("IndexArticle");
                #endregion
            }
            catch
            {
                #region (Error)
                this.ErrorAlert("عملیات با شکست مواجه شد!");
                return RedirectToAction("IndexArticle");
                #endregion
            }
        }
        #endregion
        #endregion

        #region (Edit)
        #region (GET)
        [HttpGet("Admin/Article/Edit/{Id?}")]
        public IActionResult EditArticle(int Id)
        {
            var Article = _DbContext.Articles.Where(A => A.Id == Id).SingleOrDefault();
            if (Article == null)
            {
                ErrorAlert("مقاله یافت نشد!");

                return RedirectToAction("IndexArticle");
            }

            EditArticleViewModel EditArticleViewModel = new EditArticleViewModel()
            {
                Id = Article.Id,
                MetaDescription = Article.MetaDescription,
                MetaKeyword = Article.MetaKeyword,
                ShortDescription = Article.ShortDescription,
                Slug = Article.Slug,
                Title = Article.Title,
                Description = Article.Description,
                ImageName = Article.ImageName,
            };

            return View(EditArticleViewModel);
        }
        #endregion

        #region (POST)
        [HttpPost("Admin/Article/Edit/{Id?}")]
        public IActionResult EditArticle(EditArticleViewModel EditArticleViewModel)
        {
            var Article = _DbContext.Articles.Where(A => A.Id == EditArticleViewModel.Id).SingleOrDefault();

            if (Article == null)
            {

                ErrorAlert("مقاله یافت نشد!");

                return RedirectToAction("IndexArticle");
            }

            if (EditArticleViewModel.Image == null)
            {
                Article.Title = EditArticleViewModel.Title;
                Article.Slug = EditArticleViewModel.Slug;
                Article.ShortDescription = EditArticleViewModel.ShortDescription;
                Article.MetaKeyword = EditArticleViewModel.MetaKeyword;
                Article.MetaDescription = EditArticleViewModel.MetaDescription;
                Article.Description = EditArticleViewModel.Description;
                Article.ImageAlt = EditArticleViewModel.ImageAlt;
            }

            try
            {
                // delete old image 
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.ArticleUploadPath, Article.ImageName);
                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.ArticleThumbUploadPath, Article.ImageName);

                if (System.IO.File.Exists(imagePath) && System.IO.File.Exists(imageThumbPath))
                {
                    System.IO.File.Delete(imageThumbPath);
                    System.IO.File.Delete(imagePath);
                }

                Article.Title = EditArticleViewModel.Title;
                Article.Slug = EditArticleViewModel.Slug;
                Article.ShortDescription = EditArticleViewModel.ShortDescription;
                Article.MetaKeyword = EditArticleViewModel.MetaKeyword;
                Article.MetaDescription = EditArticleViewModel.MetaDescription;
                Article.Description = EditArticleViewModel.Description;
                Article.ImageAlt = EditArticleViewModel.ImageAlt;


                Article.ImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(EditArticleViewModel.Image.FileName);

                EditArticleViewModel.Image.AddImageToServer(Article.ImageName, FilePath.ArticleUploadPath, 200, 200, FilePath.ArticleThumbUploadPath);



                _DbContext.Update(Article);

                _DbContext.SaveChanges();
            }
            catch
            {
                ErrorAlert("عملیات با شکست مواجه شد!");

                return RedirectToAction("IndexArticle");
            }

            return RedirectToAction("IndexArticle");
        }

        #endregion
        #endregion

        #region (Delete)
        [HttpPost("Admin/Article/Delete/{Id?}")]
        public IActionResult DeleteArticle(int Id)
        {
            var article = _DbContext.Articles.Where(A => A.Id == Id).SingleOrDefault();
            if (article == null)
            {
                ErrorAlert("مقاله یافت نشد!");

                return RedirectToAction("Index");
            }

            try
            {
                _DbContext.Articles.Remove(article);
                _DbContext.SaveChanges();

                // delete  image 
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.ArticleUploadPath, article.ImageName);
                var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), FilePath.ArticleThumbUploadPath, article.ImageName);

                if (System.IO.File.Exists(imagePath) && System.IO.File.Exists(imageThumbPath))
                {
                    System.IO.File.Delete(imageThumbPath);
                    System.IO.File.Delete(imagePath);
                }
            }
            catch
            {
                ErrorAlert("عملیات با شکست مواجه شد!");

                return RedirectToAction("IndexArticle");

            }

            return RedirectToAction("IndexArticle");
        }
        #endregion

        #region (Detail)
        #region (GET)
        [HttpGet("Admin/Article/Detail/{Id?}")]
        public IActionResult DetailArticle(int Id)
        {
            var Article = _DbContext.Articles.Where(A => A.Id == Id).SingleOrDefault();
            if (Article == null)
            {
                ErrorAlert("مقاله یافت نشد!");

                return RedirectToAction("IndexArticle");
            }

            DetailArticleViewModel DetailArticleViewModel = new DetailArticleViewModel()
            {
                Id = Article.Id,
                MetaDescription = Article.MetaDescription,
                MetaKeyword = Article.MetaKeyword,
                ShortDescription = Article.ShortDescription,
                Slug = Article.Slug,
                Title = Article.Title,
                ImageName = Article.ImageName,
                Visit = Article.Visit,
                CratedDate = Article.CreatedDate,
            };

            return View(DetailArticleViewModel);
        }
        #endregion
        #endregion

        #region (Filter)
        public ArticleFilterViewModel FilterArticleAsync(ArticleFilterViewModel filter)
        {
            var query = _DbContext.Articles.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(r => EF.Functions.Like(r.Title, $"%{filter.Title}%"));
            }
            #endregion

            #region  Paging
            filter.Build(query.Count()).SetEntities(query);
            #endregion
            return filter;
        }
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
