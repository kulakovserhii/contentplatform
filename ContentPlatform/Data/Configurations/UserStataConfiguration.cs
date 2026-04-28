using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class UserStataConfiguration : IEntityTypeConfiguration<UserStats>
    {
        public void Configure(EntityTypeBuilder<UserStats> builder)
        {
            builder.HasKey(us => us.Id);
            builder.HasIndex(us => us.UserId).IsUnique();
            builder.HasOne(us => us.User)
                .WithOne(u => u.UserStats)
                .HasForeignKey<UserStats>(us => us.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(us => us.FilmsReviewsCount).HasDefaultValue(0);
            builder.Property(us => us.MusicReviewsCount).HasDefaultValue(0);
            builder.Property(us => us.GameReviewsCount).HasDefaultValue(0);
            builder.Property(us => us.BookReviewsCount).HasDefaultValue(0);
            builder.Property(us => us.TvShowReviewsCount).HasDefaultValue(0);
            builder.Property(us => us.LikeReviewsCount).HasDefaultValue(0);
            builder.Property(us => us.LikeReviewsRetrievedCount).HasDefaultValue(0);
        }
    }
}
