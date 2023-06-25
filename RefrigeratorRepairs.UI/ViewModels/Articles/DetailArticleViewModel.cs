using System;

namespace RefrigeratorRepairs.UI.ViewModels.Articles
{
    public class DetailArticleViewModel
    {
        #region (Fields)
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int Visit { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string MetaDescription { get; set; }

        public string MetaKeyword { get; set; }

        public string Slug { get; set; }

        public DateTime CratedDate { get; set; }

        #endregion
    }
}
