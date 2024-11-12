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

        public async Task<object> GetCategoryByIdAsync(int id)
        {
            var categoryWithProducts = await _categories
                .Where(c => c.CategoryId == id)
                .Select(c => new
                {
                    CategoryName = c.CategoryName,
                    Products = c.ProductCategories.Select(pc => pc.Product.Name).ToList()
                })
                .FirstOrDefaultAsync();

            return categoryWithProducts;
        }

        public async Task<List<object>> GetCategoryList()
        {
            var categoriesWithProducts = await _categories
                .Select(c => new
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    ProductCategories = c.ProductCategories.Count() // عدد المنتجات في الفئة
                })
                .ToListAsync();

            return categoriesWithProducts.Cast<object>().ToList();
        }

    }
}
