namespace RefrigeratorRepairs.MODEL.Entities.User
{
    public class UserRole : BaseEntity
    {
        #region (Relation)
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
        #endregion


    }
}
