using System.Collections.Generic;

namespace RefrigeratorRepairs.MODEL.Entities.User
{
    public class Role : BaseEntity
    {
        #region (Fields)
        public string Title { get; set; }
        #endregion

        #region (Relation)
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
