namespace E_CommerceProject.Core.DTOs.OrderDTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ListOrderItem> OrderItems { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class ListOrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}