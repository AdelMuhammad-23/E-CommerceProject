﻿using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace E_CommerceProject.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public Task<PaginatedResult<ProductDto>> GetProductsAsync(
           int pageNumber = 1,
           int pageSize = 10,
           string? nameFilter = null,
           decimal? priceFilter = null);
        public Task<string> AddProductAsync(Product product, IFormFile productImage);
        public Task<List<ProductsListDTO>> GetProductListAsync();
        public Task<string> UpdateProductAsync(Product product, IFormFile productImage);

    }
}
