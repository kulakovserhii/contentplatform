using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
    {
        public void Configure(EntityTypeBuilder<TVShow> builder)
        {
            builder.HasMany(ts => ts.Episodes)
                .WithOne(e => e.TVShow)
                .HasForeignKey(e => e.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
