using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceProject.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Payment-Order relationship
            builder.HasOne(p => p.Order)
                            .WithOne(o => o.Payment)
                            .HasForeignKey<Payment>(p => p.OrderId)
                                    .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete


            // Payment-User relationship
            builder.HasOne(p => p.User)
                            .WithMany(u => u.Payments)
                            .HasForeignKey(p => p.UserId)
                            .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete

        }
    }
}
