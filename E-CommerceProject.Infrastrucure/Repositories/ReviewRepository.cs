using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReviewRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateReviewAsync(Review addReview)
        {
            await _dbContext.Reviews.AddAsync(addReview);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
    }
}
