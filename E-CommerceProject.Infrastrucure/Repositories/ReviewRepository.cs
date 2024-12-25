using E_CommerceProject.Core.DTOs.Pagination;
using E_CommerceProject.Core.DTOs.ReviewsDTOs;
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

        public async Task<string> DeleteReviewAsync(Review deleteReview)
        {
            _reviews.Remove(deleteReview);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<PaginatedResult<ReviewsPaginatedResultDTO>> GetPaginationReviews(int pageNumber = 1, int pageSize = 10)
        {
            var query = _reviews.AsQueryable();

            var totalItems = await query.CountAsync();

            var reviews = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReviewsPaginatedResultDTO
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    ProductId = r.ProductId,
                    Rating = r.Rating.Value,
                    Comment = r.Comment
                })
                .ToListAsync();
            return new PaginatedResult<ReviewsPaginatedResultDTO>
            {
                Items = reviews,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }

        public async Task<string> UpdateReviewAsync(Review updateReview)
        {
            _reviews.Update(updateReview);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
    }
}
