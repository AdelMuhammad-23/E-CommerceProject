using E_CommerceProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceProject.Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // Address-User relationship
            builder.HasOne(a => a.User)
                            .WithMany(u => u.Addresses)
                            .HasForeignKey(a => a.UserId);
        }
    }
}
