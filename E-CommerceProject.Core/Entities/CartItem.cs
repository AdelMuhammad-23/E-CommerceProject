namespace E_CommerceProject.Core.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
