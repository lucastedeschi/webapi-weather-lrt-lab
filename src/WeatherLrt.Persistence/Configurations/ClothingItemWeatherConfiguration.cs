using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherLrt.Domain.Entities;
using WeatherLrt.Domain.Enumerations;

namespace WeatherLrt.Persistence.Configurations
{
    public sealed class ClothingItemWeatherConfiguration : IEntityTypeConfiguration<ClothingItemWeather>
    {
        public void Configure(EntityTypeBuilder<ClothingItemWeather> builder)
        {
            builder.ToTable("ClothingItemWeather");

            builder.Property(e => e.WeatherType)
                .HasMaxLength(4)
                .IsRequired()
                .HasColumnType("varchar(4)")
                .HasConversion(
                    v => v.ToString(),
                    v => (WeatherType)Enum.Parse(typeof(WeatherType), v));

            builder.HasOne(e => e.ClothingItem)
                .WithMany(e => e.ClothingItemWeathers)
                .HasForeignKey(e => e.ClothingItemId)
                .HasConstraintName("fkClothingItemWeather")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
