using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Application.Interfaces;

namespace WeatherLrt.Persistence
{
    public sealed class WeatherLrtContext : DbContext, IWeatherLrtContext
    {
        public WeatherLrtContext(DbContextOptions<WeatherLrtContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<SystemUser> SystemUsers { get; set; }

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
            modelBuilder.Entity<SystemUser>()
                .HasKey(e => e.SystemUserId)
                .IsClustered(false)
                .HasName("pkSystemUser");

        }
    }
}
