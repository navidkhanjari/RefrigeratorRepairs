
using System.Collections.Generic;

namespace RefrigeratorRepairs.MODEL.Entities.User
{
    public class User : BaseEntity
    {
        #region (Fields)
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        #endregion
    }

    public enum Role
    {
        Admin = 0,
    }
}
