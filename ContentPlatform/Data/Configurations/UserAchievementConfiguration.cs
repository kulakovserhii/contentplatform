using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class UserAchievementConfiguration : IEntityTypeConfiguration<UserAchevement>
    {
        public void Configure(EntityTypeBuilder<UserAchevement> builder)
        {
            builder.HasKey(ua => ua.Id);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.UserAchevements)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ua => ua.Achievement)
                .WithMany()
                .HasForeignKey(ua => ua.AchievementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ua => ua.UnlockedAt).HasDefaultValueSql ("GETUTCDATE()");
        }
    }
}
