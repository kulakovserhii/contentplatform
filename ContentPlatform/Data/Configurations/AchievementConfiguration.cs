using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name).IsRequired().HasMaxLength(70);
            builder.Property(a => a.Description).IsRequired().HasMaxLength(300);
            builder.Property(a => a.BadgeTitle).HasMaxLength(50);

            builder.Property(a => a.Category).IsRequired();
        }
    }
}
