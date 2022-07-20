using loadTestApi.Model;
using Microsoft.EntityFrameworkCore;

namespace loadTestApi.DataAccess
{
    public class PostgreDBContext : DbContext
    {
        public PostgreDBContext(DbContextOptions<PostgreDBContext> options) : base(options)
        {
        }

        public DbSet<LoadData> loadData { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LoadData>();
        }
    }
}

