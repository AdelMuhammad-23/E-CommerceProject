using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Where(product => product.ProductId.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsListAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }
    }
}
