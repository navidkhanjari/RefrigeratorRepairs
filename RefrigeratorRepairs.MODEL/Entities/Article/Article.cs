using System;
using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.MODEL.Entities.Article
{
    public class Article : BaseEntity
    {
        #region (Fields)
        public string ImageName { get; set; }
        public string ImageAlt { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string ShortDescription { get; set; }
        
        public string MetaDescription { get; set; }
        public string MetaKeyword { get; set; }
        
        [Required]
        public string Slug { get; set; }
        public int Visit { get; set; }
        public DateTime CreatedDate { get; set; }
        #endregion
    }
}
