using E_CommerceProject.Core.DTOs.Pagination;
using E_CommerceProject.Core.DTOs.ReviewsDTOs;
using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        public Task<PaginatedResult<ReviewsPaginatedResultDTO>> GetPaginationReviews(int pageNumber = 1, int pageSize = 10);
        public Task<string> CreateReviewAsync(Review addReview);
        public Task<string> UpdateReviewAsync(Review updateReview);
        public Task<string> DeleteReviewAsync(Review deleteReview);
    }
}
