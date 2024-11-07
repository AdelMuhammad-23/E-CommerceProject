using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceProject.Infrastructure.Configurations
{
    public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
    {
        public void Configure(EntityTypeBuilder<Wishlist> builder)
        {

            // Wishlist-User relationship
            builder.HasOne(w => w.User)
                 .WithMany(u => u.Wishlists)
                 .HasForeignKey(w => w.UserId);


        }
    }
}
