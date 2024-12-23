namespace E_CommerceProject.Core.DTOs.ReviewsDTOs
{
    public class CreateReviewDTO
    {
        public int ProductId { get; set; }
        public Rate Rating { get; set; }
        public string Comment { get; set; }
    }
    public enum Rate
    {
        Terrible,
        Bad,
        Average,
        Good,
        VeryGood,
        Excellent
    }
}
