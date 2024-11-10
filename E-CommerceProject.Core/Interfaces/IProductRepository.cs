using E_CommerceProject.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<string> AddProductAsync(Product product, IFormFile productImage);
    }
}
