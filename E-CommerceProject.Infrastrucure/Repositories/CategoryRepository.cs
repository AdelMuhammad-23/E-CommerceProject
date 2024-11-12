using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private DbSet<Category> _categories;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _categories = context.Set<Category>();
        }

        public async Task<string> AddCategoryAsync(Category category)
        {
            if (!_categories.Any(c => c.CategoryName == category.CategoryName))
            {
                await _categories.AddAsync(category);
                await _dbContext.SaveChangesAsync();
                return "Added Successfully";
            }
            return "Category already exists";
        }


    }
}
