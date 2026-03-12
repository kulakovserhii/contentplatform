using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContentPlatform.Data.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.Reviews)
                .WithOne(r => r.Content)
                .HasForeignKey(c => c.ContentId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
