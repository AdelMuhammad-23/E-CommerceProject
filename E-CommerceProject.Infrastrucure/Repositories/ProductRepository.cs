using AutoMapper;
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
            var context = _contextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var imageUrl = await _fileServies.UploadImage("Products", productImage);

            switch (imageUrl)
            {
                case "this extension is not allowed":
                    return "this extension is not allowed";
                case "this image is too big":
                    return "this image is too big";
                case "FailedToUploadImage":
                    return "FailedToUploadImage";
            }
            product.Image = baseUrl + imageUrl;
            await _products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }
    }
}
