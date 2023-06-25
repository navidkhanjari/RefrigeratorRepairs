using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.UI.ViewModels.Articles
{
    public class EditArticleViewModel
    {
        #region (Fields)
        public int Id { get; set; }
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string ImageAlt { get; set; }

        [MaxLength(50, ErrorMessage = "عنوان نباید بیشتر از 50 کاراکتر باشد")]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        [Display(Name = "توضیحات")]
        [UIHint("Ckeditor4")]
        public string Description { get; set; }

        [MaxLength(100, ErrorMessage = "توضیح کوتاه نباید بیشتر از 100 کارکتر باشد")]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string ShortDescription { get; set; }

        [MaxLength(100, ErrorMessage = "توضیح کوتاه نباید بیشتر از 100 کارکتر باشد")]
        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string MetaKeyword { get; set; }

        [Required(ErrorMessage = "وارد کردن این فیلد اجباری است!")]
        public string Slug { get; set; }

        public string ImageName { get; set; }
        #endregion
    }
}
