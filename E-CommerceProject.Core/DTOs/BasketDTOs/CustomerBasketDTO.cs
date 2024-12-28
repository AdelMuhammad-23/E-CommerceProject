namespace E_CommerceProject.Core.DTOs.BasketDTOs
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketListDTO> BasketItems { get; set; } = new List<BasketListDTO>();

    }
    public class BasketListDTO
    {
        public string productName { get; set; }
        public decimal price { get; set; }
        public int Quantity { get; set; }
        public string picture { get; set; }
        public string categoryName { get; set; }
    }
}
