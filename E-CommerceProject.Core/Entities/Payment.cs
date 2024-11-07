using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public User User { get; set; }
    }
}
