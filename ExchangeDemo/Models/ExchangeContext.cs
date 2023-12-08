using Microsoft.EntityFrameworkCore;

namespace ExchangeDemo.Models
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext(DbContextOptions<ExchangeContext> options): base(options) { }
        public DbSet<RateEntity> Rates { get; set; }
        public DbSet<TimestampEntity> TimeStamps { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RateEntity>();
            builder.Entity<TimestampEntity>();
        }
    }
}
