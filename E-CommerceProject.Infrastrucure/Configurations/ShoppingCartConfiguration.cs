using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceProject.Infrastructure.Configurations
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            // ShoppingCart-User relationship
            builder.HasOne(sc => sc.User)
                            .WithOne(u => u.ShoppingCart)
                            .HasForeignKey<ShoppingCart>(sc => sc.UserId);
        }
    }
}
