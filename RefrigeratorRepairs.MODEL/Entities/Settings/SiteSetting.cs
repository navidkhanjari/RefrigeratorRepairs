using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.MODEL.Entities.Settings
{
    public class SiteSetting : BaseEntity
    {
        #region (Fields)
        [Required]
        public string Background { get; set; }

        [Required]
        public string TextInBackground { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string WhatWeDo { get; set; }

        [Required]
        public string AboutUs { get; set; }
        #endregion
    }
}
