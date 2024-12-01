namespace E_CommerceProject.Core.DTOs.OrderDTOs
{
    public class AddOrderDTO
    {
        public List<OrderItemDTO> OrderItems { get; set; }
    }
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
