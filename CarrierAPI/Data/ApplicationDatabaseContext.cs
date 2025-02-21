using CarrierAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace CarrierAPI.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrier>()
                .HasMany(e => e.CarrierConfigurations)
                .WithOne(e => e.Carrier)
                .HasForeignKey(e => e.CarrierId);

            modelBuilder.Entity<Carrier>()
                .HasMany(e => e.Orders)
                .WithOne(e => e.Carrier)
                .HasForeignKey(e => e.CarrierId);

            modelBuilder.Entity<Carrier>()
                .HasMany(e => e.CarrierReports)
                .WithOne(e => e.Carrier)
                .HasForeignKey(e => e.CarrierId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }
    }
}
