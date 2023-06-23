using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefrigeratorRepairs.MODEL.Entities.Settings
{
    public class SiteSetting : BaseEntity
    {
        #region (Fields)
        public string Background { get; set; }
        public string TextInBackground { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string WhatWeDo { get; set; }
        public string AboutUs { get; set; }
        #endregion
    }
}
