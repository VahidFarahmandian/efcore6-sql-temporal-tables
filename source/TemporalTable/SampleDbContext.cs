using TemporalTable.Models;

namespace TemporalTable
{
    public class SampleDbContext : DbContext
    {
        private const string connectionString = "Data Source=.;Initial Catalog=SampleDb;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connectionString);

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .ToTable("Products", b => b.IsTemporal(t =>
                {
                    t.HasPeriodStart("ValidFrom");
                    t.HasPeriodEnd("ValidTo");
                    t.UseHistoryTable("ProductHistoricalData");
                }));
        }
    }
}
