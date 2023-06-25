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
        public DbSet<Entities.Settings.SiteSetting> SiteSettings { get; set; }
        #endregion
    }
}
