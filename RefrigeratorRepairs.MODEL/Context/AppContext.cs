using Microsoft.EntityFrameworkCore;

namespace RefrigeratorRepairs.MODEL.Context
{
    public class RRContext : DbContext
    {
        #region (Constructor)
        public RRContext(DbContextOptions<RRContext> options) : base(options)
        {

        }
        #endregion

        #region (Properties)
        public DbSet<Entities.Article.Article> Articles { get; set; }
        public DbSet<Entities.User.User> Users { get; set; }
        public DbSet<Entities.User.UserRole> UserRoles { get; set; }
        public DbSet<Entities.User.RolePermission> RolePermissions { get; set; }
        public DbSet<Entities.User.Role> Roles { get; set; }
        public DbSet<Entities.User.Permission> Permissions { get; set; }
        #endregion
    }
}
