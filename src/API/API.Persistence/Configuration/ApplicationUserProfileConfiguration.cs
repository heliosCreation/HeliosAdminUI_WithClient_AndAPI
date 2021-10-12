using API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Persistence.Configuration
{
    public class ApplicationUserProfileConfiguration : IEntityTypeConfiguration<ApplicationUserProfile>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserProfile> builder)
        {
            builder.HasIndex(e => e.Id);
            builder.HasIndex(e => e.Subject);

            builder.Property(e => e.SubscriptionLevel)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
