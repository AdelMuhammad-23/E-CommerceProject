using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


namespace E_CommerceProject.Infrastructure.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly DbSet<Review> _reviews;
        public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _reviews = dbContext.Set<Review>();
        }
        public async Task<string> CreateReviewAsync(Review addReview)
        {
            await _reviews.AddAsync(addReview);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> UpdateReviewAsync(Review updateReview)
        {
            _reviews.Update(updateReview);

            await _dbContext.SaveChangesAsync();
            return "Success";
        }
    }
}
