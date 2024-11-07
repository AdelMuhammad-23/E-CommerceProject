using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Entities
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<WishlistItem>? WishlistItems { get; set; }
    }
}
