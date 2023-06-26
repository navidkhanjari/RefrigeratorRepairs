
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefrigeratorRepairs.MODEL.Entities.User
{
    public class User : BaseEntity
    {
        #region (Fields)
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }
        public Role Role { get; set; }
        #endregion
    }

    public enum Role
    {
        Admin = 0,
        None = 1,
    }
}
