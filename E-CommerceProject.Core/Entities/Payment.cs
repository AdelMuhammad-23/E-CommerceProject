using E_CommerceProject.Core.Entities.Identity;
using E_CommerceProject.Core.Enums;
using System.Text.Json.Serialization;

namespace E_CommerceProject.Core.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Currency { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string PaymentIntentId { get; set; }
        public string TransactionId { get; set; }

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }

}
