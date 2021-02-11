using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherLrt.Domain.Entities;

namespace WeatherLrt.Persistence.Configurations
{
    public sealed class ClothingItemConfiguration : IEntityTypeConfiguration<ClothingItem>
    {
        public void Configure(EntityTypeBuilder<ClothingItem> builder)
        {
            builder.ToTable("ClothingItem");

            builder.Property(e => e.Description)
                .HasMaxLength(60)
                .IsRequired()
                .HasColumnType("varchar(60)");

            builder.HasIndex(e => e.Description)
                .HasDatabaseName("ixClothingItemDescription");
        }
    }
}
