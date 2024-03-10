using Microsoft.EntityFrameworkCore;

namespace DbLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        internal DbSet<DbUserInfo> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<DbUserInfo>().ToTable("users");

        }
    }
}
