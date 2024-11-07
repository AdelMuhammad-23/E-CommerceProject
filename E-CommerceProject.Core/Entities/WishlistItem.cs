namespace E_CommerceProject.Core.Entities
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public int WishlistId { get; set; }
        public int ProductId { get; set; }

        public virtual Wishlist? Wishlist { get; set; }
        public virtual Product? Product { get; set; }
    }
}
