using DbLayer.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace DbLayer
{
    // Host Db Context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        internal DbSet<DbUserInfo> Users { get; set; }

        // Prefer this over 
        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);
            mb.Entity<DbUserInfo>().ToTable("users");

        }
    }
}
