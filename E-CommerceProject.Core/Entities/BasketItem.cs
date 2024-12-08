namespace E_CommerceProject.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public string picture { get; set; }
        public string categoryName { get; set; }
    }
}
