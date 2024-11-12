using AutoMapper;
using E_CommerceProject.Core.DTOs;
using E_CommerceProject.Core.Entities;
using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        #region Fields
        private readonly DbSet<Product> _products;
        private readonly IMapper _mapper;
        private readonly IFileService _fileServies;
        private readonly IHttpContextAccessor _contextAccessor;
        #endregion

        public ProductRepository(ApplicationDbContext context,
                                    IMapper mapper,
                                  IFileService fileServies,
                                  IHttpContextAccessor contextAccessor) : base(context)
        {

            _products = context.Set<Product>();
            _mapper = mapper;
            _fileServies = fileServies;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> AddProductAsync(Product product, IFormFile productImage)
        {
            //call function for handle Product image
            var newImage = imageService(productImage);
            product.Image = newImage;

            await _products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }

        public async Task<List<ProductsListDTO>> GetProductListAsync()
        {
            var productDtos = await _products
                                            .AsNoTracking()
                                            .Select(p => new ProductsListDTO
                                            {
                                                Name = p.Name,
                                                Description = p.Description,
                                                Image = p.Image,
                                                Price = p.Price,
                                                Stock = p.Stock,
                                                CreatedAt = p.CreatedAt,
                                                CategoryName = p.ProductCategories.FirstOrDefault().Category.CategoryName
                                            })
                                            .ToListAsync();


            return productDtos;
        }

        public async Task<string> UpdateProductAsync(Product product, IFormFile productImage)
        {

            if (productImage != null)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    var isDeleted = _fileServies.DeleteImage("Products", product.Image);
                    if (!isDeleted)
                        return "Failed to delete the old image.";
                }

                var newImage = imageService(productImage);

                product.Image = newImage;
            }
            _products.Update(product);
            await _dbContext.SaveChangesAsync();
            return "Updated product successfully";
        }

        #region Handel Images
        public string imageService(IFormFile productImage)
        {
            var context = _contextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = _fileServies.UploadImage("Products", productImage);

            switch (imageUrl)
            {
                case "this extension is not allowed":
                    return "this extension is not allowed";
                case "this image is too big":
                    return "this image is too big";
                case "FailedToUploadImage":
                    return "FailedToUploadImage";
            }
            var URL = baseUrl + imageUrl;
            return URL;
        }
        #endregion
    }
}
