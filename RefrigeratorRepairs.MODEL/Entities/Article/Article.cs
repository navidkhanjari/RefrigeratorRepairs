using System;

namespace RefrigeratorRepairs.MODEL.Entities.Article
{
    public class Article : BaseEntity
    {
        #region (Fields)
        public string ImageName { get; set; }
        public string ImageAlt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        public string Slug { get; set; }
        public int Visit { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
