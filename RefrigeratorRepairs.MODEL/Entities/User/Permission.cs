using System.Collections.Generic;

namespace RefrigeratorRepairs.MODEL.Entities.User
{
    public class Permission : BaseEntity
    {
        #region (Fields)
        public string Title { get; set; }
        #endregion

        #region (Relation)
        public ICollection<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
