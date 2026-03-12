using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasOne(e => e.TVShow)
                .WithMany(ts => ts.Episodes)
                .HasForeignKey(e => e.TVShowId);
            builder.HasOne<Content>()
                .WithOne()
                .HasForeignKey<Episode>(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
