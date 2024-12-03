using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Enums;

namespace E_CommerceProject.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = OrderStatus.Pending.ToString();
        public decimal TotalPrice { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }

    }
}
