using Microsoft.EntityFrameworkCore;
using TemporalTable.Models;

namespace TemporalTable
{
    public class SampleDbContext : DbContext
    {
        private const string connectionString = "Data Source=.;Initial Catalog=SampleDb;Integrated Security=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(connectionString);

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                   .Entity<Customer>()
                   .ToTable("Customers", b => b.IsTemporal());

            modelBuilder
                .Entity<Product>()
                .ToTable("Products", b => b.IsTemporal());

            modelBuilder
                .Entity<Order>()
                .ToTable("Orders", b => b.IsTemporal());
        }
    }
}
