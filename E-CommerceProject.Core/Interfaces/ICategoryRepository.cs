using E_CommerceProject.Core.Entities;

namespace E_CommerceProject.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

        public Task<object> GetCategoryByIdAsync(int id);
        public Task<List<object>> GetCategoryList();

        public Task<string> AddCategoryAsync(Category category);
    }
}
