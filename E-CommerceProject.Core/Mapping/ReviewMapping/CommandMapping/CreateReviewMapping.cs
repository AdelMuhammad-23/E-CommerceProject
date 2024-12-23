using E_CommerceProject.Core.DTOs.ReviewsDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile
    {
        public void CreateReviewMapping()
        {
            CreateMap<CreateReviewDTO, Review>();
        }
    }
}
