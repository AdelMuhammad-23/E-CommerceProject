using E_CommerceProject.Core.Enums;

namespace E_CommerceProject.Core.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = OrderStatus.Pending.ToString();
        public ICollection<OrderItemDTO> orderItem { get; set; }
    }
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
