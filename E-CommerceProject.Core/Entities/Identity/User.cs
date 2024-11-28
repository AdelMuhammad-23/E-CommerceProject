using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Core.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Code { get; set; }
        public virtual ICollection<Order>? Orders { get; set; } = new List<Order>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Address>? Addresses { get; set; } = new List<Address>();
        public virtual ShoppingCart? ShoppingCart { get; set; }
        public virtual ICollection<Wishlist>? Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Payment>? Payments { get; set; }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> userRefreshToken { get; set; }

    }
}
