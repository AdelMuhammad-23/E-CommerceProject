using E_CommerceProject.Core.DTOs.OrderDTOs;

namespace E_CommerceProject.Core.Responses
{
    public class OrderResponse
    {
        public int UserId { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
