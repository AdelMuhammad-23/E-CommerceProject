namespace E_CommerceProject.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public virtual ICollection<ProductCategory>? ProductCategories { get; set; } = new List<ProductCategory>();
        public virtual ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();
        public virtual ICollection<WishlistItem>? WishlistItems { get; set; } = new List<WishlistItem>();

    }

}
