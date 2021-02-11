using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Application.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WeatherLrt.Persistence
{
    public sealed class WeatherLrtContext : IdentityDbContext<ApplicationUser>, IWeatherLrtContext
    {
        public WeatherLrtContext(DbContextOptions<WeatherLrtContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ClothingItem> ClothingItems { get; set; }

        public DbSet<ClothingItemWeather> ClothingItemWeathers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Auditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ModifiedOn = entry.Entity.CreatedOn = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClothingItem>()
                .HasKey(e => e.ClothingItemId)
                .HasName("pkClothingItem");

            modelBuilder.Entity<ClothingItemWeather>()
                .HasKey(e => e.ClothingItemWeatherId)
                .HasName("pkClothingItemWeather");

            modelBuilder.Entity<ClothingItemWeather>()
                .HasIndex(e => new { e.ClothingItemId, e.WeatherType })
                .IsUnique()
                .HasDatabaseName("ukClothingItemWeather");
        }
    }
}
