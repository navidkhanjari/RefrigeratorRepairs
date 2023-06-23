namespace RefrigeratorRepairs.MODEL.Entities.User
{
    public class RolePermission : BaseEntity
    {
        #region (Relations)
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
        #endregion

    }
}
