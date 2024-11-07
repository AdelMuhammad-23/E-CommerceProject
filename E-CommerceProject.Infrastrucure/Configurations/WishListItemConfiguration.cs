using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceProject.Infrastructure.Configurations
{
    public class WishListItemConfiguration : IEntityTypeConfiguration<WishlistItem>
    {
        void IEntityTypeConfiguration<WishlistItem>.Configure(EntityTypeBuilder<WishlistItem> builder)
        {
            // WishlistItem-Wishlist relationship
            builder.HasOne(wi => wi.Wishlist)
                .WithMany(w => w.WishlistItems)
                .HasForeignKey(wi => wi.WishlistId);

            // WishlistItem-Product relationship
            builder.HasOne(wi => wi.Product)
            .WithMany(p => p.WishlistItems)
            .HasForeignKey(wi => wi.ProductId);
        }
    }
}
