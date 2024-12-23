using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IReviewRepository
    {
        public Task<string> CreateReviewAsync(Review addReview);
    }
}
