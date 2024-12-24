using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        public Task<string> CreateReviewAsync(Review addReview);
        public Task<string> UpdateReviewAsync(Review updateReview);
        //public Task<string> DeleteReviewAsync(Review addReview);

    }
}
