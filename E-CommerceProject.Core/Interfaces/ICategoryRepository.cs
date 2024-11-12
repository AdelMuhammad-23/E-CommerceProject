using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<string> AddCategoryAsync(Category category);
    }
}
