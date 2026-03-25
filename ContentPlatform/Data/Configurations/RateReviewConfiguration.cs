using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class RateReviewConfiguration : IEntityTypeConfiguration<RateReview>
    {
        public void Configure(EntityTypeBuilder<RateReview> builder)
        {
            builder.HasKey(rr => rr.Id);
            builder.HasIndex(rr => new { rr.UserId, rr.ReviewId }).IsUnique();
            builder.HasOne(rr => rr.User)
                .WithMany(rr => rr.RateReviews)
                .HasForeignKey(rr => rr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(rr => rr.Review)
                .WithMany(rr => rr.RateReviews)
                .HasForeignKey(rr => rr.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
