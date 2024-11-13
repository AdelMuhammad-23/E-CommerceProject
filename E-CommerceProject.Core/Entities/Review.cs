using E_CommerceProject.Core.Entities.Identity;

namespace E_CommerceProject.Core.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual User? User { get; set; }
        public virtual Product? Product { get; set; }
    }
}
