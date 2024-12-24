using AutoMapper;

namespace E_CommerceProject.Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateReviewMapping();
            UpdateReviewMapping();
        }
    }
}
