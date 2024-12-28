namespace E_CommerceProject.Core.DTOs.ReviewsDTOs
{
    public class ReviewsPaginatedResultDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
