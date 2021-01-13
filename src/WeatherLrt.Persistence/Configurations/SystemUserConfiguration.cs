using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Persistence.Configurations
{
    public sealed class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.ToTable("SystemUser");

            builder.Property(e => e.Name)
                .HasMaxLength(120)
                .IsRequired()
                .HasColumnType("varchar(120)");

            builder.Property(e => e.Email)
                .HasMaxLength(120)
                .IsRequired()
                .HasColumnType("varchar(120)");

            builder.HasIndex(e => e.Email)
                .HasDatabaseName("ixSystemUserEmail");
        }
    }
}
