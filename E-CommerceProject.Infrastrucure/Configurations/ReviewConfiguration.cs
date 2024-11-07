using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceProject.Infrastructure.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        void IEntityTypeConfiguration<Review>.Configure(EntityTypeBuilder<Review> builder)
        {
            // Review-User relationship
            builder.HasOne(r => r.User)
                            .WithMany(u => u.Reviews)
                            .HasForeignKey(r => r.UserId);

            // Review-Product relationship
            builder.HasOne(r => r.Product)
                            .WithMany(p => p.Reviews)
                            .HasForeignKey(r => r.ProductId);
        }
    }
}
