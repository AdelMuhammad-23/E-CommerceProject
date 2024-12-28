using E_CommerceProject.Core.Enums;

namespace E_CommerceProject.Core.DTOs.ReviewsDTOs

{
    public class CreateReviewDTO
    {
        public int ProductId { get; set; }
        public RateEnum? Rating { get; set; }
        public string? Comment { get; set; }
    }


}
